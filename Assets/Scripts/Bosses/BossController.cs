using UnityEngine;

namespace IslasApocalipsis.Bosses
{
    /// <summary>
    /// Boss types in the game
    /// </summary>
    public enum BossType
    {
        FallenSkyLord,
        CorruptedTitanGolem,
        ShadowKing,
        AncientTechColossus,
        IceEmpress,
        FireDrake,
        StormSovereign,
        VoidEntity,
        CrystalHivemind,
        LastGuardian
    }

    /// <summary>
    /// Base boss controller with multiple phases
    /// </summary>
    public class BossController : MonoBehaviour
    {
        [Header("Boss Info")]
        public BossType bossType;
        public string bossName = "Boss";
        public int bossLevel = 10;

        [Header("Stats")]
        public float maxHealth = 1000f;
        public float currentHealth;
        public float[] phaseHealthThresholds = { 0.7f, 0.4f }; // Health % for phase changes
        public int currentPhase = 1;

        [Header("Combat")]
        public float baseDamage = 30f;
        public float attackRange = 5f;
        public float enrageMultiplier = 1.5f;
        public bool isEnraged = false;

        [Header("Resistances")]
        public float physicalResistance = 0.2f;
        public float magicResistance = 0.2f;

        [Header("Arena")]
        public Transform[] arenaPoints;
        public float arenaRadius = 30f;

        private Transform player;
        private bool isFightStarted = false;
        private bool isDefeated = false;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
        }

        protected virtual void Start()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        protected virtual void Update()
        {
            if (isDefeated) return;

            if (!isFightStarted && player != null)
            {
                CheckPlayerProximity();
            }

            if (isFightStarted)
            {
                BossAI();
                CheckPhaseTransitions();
            }
        }

        private void CheckPlayerProximity()
        {
            if (Vector3.Distance(transform.position, player.position) <= arenaRadius)
            {
                StartBossFight();
            }
        }

        protected virtual void StartBossFight()
        {
            isFightStarted = true;
            Debug.Log($"{bossName} boss fight started!");

            // Show boss health bar
            if (UI.UIManager.Instance != null)
            {
                UI.UIManager.Instance.ShowBossHealthBar(bossName, 1f);
            }

            OnBossFightStart();
        }

        protected virtual void OnBossFightStart()
        {
            // Override in specific boss implementations
        }

        public void TakeDamage(float damage, bool isMagicDamage = false)
        {
            if (isDefeated) return;

            float resistance = isMagicDamage ? magicResistance : physicalResistance;
            float actualDamage = damage * (1f - resistance);
            
            currentHealth -= actualDamage;
            currentHealth = Mathf.Max(currentHealth, 0);

            Debug.Log($"{bossName} took {actualDamage} damage. Health: {currentHealth}/{maxHealth}");

            // Update boss health bar
            if (UI.UIManager.Instance != null)
            {
                UI.UIManager.Instance.UpdateBossHealthBar(GetHealthPercent());
            }

            OnTakeDamage();

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void OnTakeDamage()
        {
            // Override for specific boss reactions
        }

        private void CheckPhaseTransitions()
        {
            float healthPercent = GetHealthPercent();

            if (currentPhase == 1 && healthPercent <= phaseHealthThresholds[0])
            {
                TransitionToPhase(2);
            }
            else if (currentPhase == 2 && healthPercent <= phaseHealthThresholds[1])
            {
                TransitionToPhase(3);
            }
        }

        protected virtual void TransitionToPhase(int newPhase)
        {
            currentPhase = newPhase;
            Debug.Log($"{bossName} entered Phase {currentPhase}!");

            OnPhaseTransition(newPhase);

            // Enrage in final phase
            if (currentPhase == 3)
            {
                isEnraged = true;
                Debug.Log($"{bossName} is enraged!");
            }
        }

        protected virtual void OnPhaseTransition(int newPhase)
        {
            // Override for specific boss phase transitions
        }

        protected virtual void BossAI()
        {
            // Override in specific boss implementations
            // Default: basic chase and attack
            if (player == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }

        protected virtual void Attack()
        {
            // Override in specific boss implementations
            float finalDamage = baseDamage * (isEnraged ? enrageMultiplier : 1f);
            
            var playerStats = player.GetComponent<Player.PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(finalDamage);
            }
        }

        protected void MoveTowardsPlayer()
        {
            if (player == null) return;

            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * 3f * Time.deltaTime;
            
            // Face player
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }

        protected virtual void Die()
        {
            if (isDefeated) return;

            isDefeated = true;
            Debug.Log($"{bossName} defeated!");

            // Hide boss health bar
            if (UI.UIManager.Instance != null)
            {
                UI.UIManager.Instance.HideBossHealthBar();
                UI.UIManager.Instance.ShowNotification($"{bossName} Defeated!", 5f);
            }

            OnDeath();

            // Destroy after animation
            Destroy(gameObject, 5f);
        }

        protected virtual void OnDeath()
        {
            // Override for specific boss death behavior
            // Drop loot, trigger cutscene, etc.
        }

        public float GetHealthPercent() => currentHealth / maxHealth;
        public bool IsDefeated() => isDefeated;

        protected virtual void OnDrawGizmosSelected()
        {
            // Draw arena radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, arenaRadius);

            // Draw attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
