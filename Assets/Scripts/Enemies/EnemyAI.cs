using UnityEngine;
using UnityEngine.AI;

namespace IslasApocalipsis.Enemies
{
    /// <summary>
    /// Enemy AI behavior types
    /// </summary>
    public enum AIBehaviorType
    {
        Aggressive,  // Always attacks player
        Defensive,   // Maintains distance
        Patrol,      // Patrols until player seen
        Stationary,  // Stays in place, attacks when in range
        Flying,      // Aerial movement
        Support      // Supports other enemies
    }

    /// <summary>
    /// Base AI controller for enemies
    /// </summary>
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("AI Settings")]
        public AIBehaviorType behaviorType = AIBehaviorType.Aggressive;
        public float detectionRange = 10f;
        public float attackRange = 2f;
        public float attackCooldown = 2f;
        public float moveSpeed = 3.5f;

        [Header("Combat")]
        public float attackDamage = 10f;
        public bool canBlock = false;
        public bool canDodge = false;

        [Header("Patrol Settings")]
        public Transform[] patrolPoints;
        public float patrolWaitTime = 2f;

        private Transform player;
        private EnemyHealth health;
        private NavMeshAgent agent;
        
        private bool hasDetectedPlayer = false;
        private float lastAttackTime;
        private int currentPatrolIndex = 0;
        private float patrolWaitTimer = 0f;
        private bool isWaiting = false;

        private void Awake()
        {
            health = GetComponent<EnemyHealth>();
            agent = GetComponent<NavMeshAgent>();
            
            if (agent != null)
            {
                agent.speed = moveSpeed;
            }
        }

        private void Start()
        {
            // Find player
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        private void Update()
        {
            if (health.IsDead()) return;

            float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : float.MaxValue;

            // Detection
            if (!hasDetectedPlayer && distanceToPlayer <= detectionRange)
            {
                hasDetectedPlayer = true;
                OnPlayerDetected();
            }

            // Behavior based on type and state
            if (hasDetectedPlayer && player != null)
            {
                ExecuteCombatBehavior(distanceToPlayer);
            }
            else
            {
                ExecuteIdleBehavior();
            }
        }

        private void ExecuteCombatBehavior(float distanceToPlayer)
        {
            switch (behaviorType)
            {
                case AIBehaviorType.Aggressive:
                    AggressiveBehavior(distanceToPlayer);
                    break;
                case AIBehaviorType.Defensive:
                    DefensiveBehavior(distanceToPlayer);
                    break;
                case AIBehaviorType.Flying:
                    FlyingBehavior(distanceToPlayer);
                    break;
                case AIBehaviorType.Support:
                    SupportBehavior(distanceToPlayer);
                    break;
                default:
                    AggressiveBehavior(distanceToPlayer);
                    break;
            }
        }

        private void AggressiveBehavior(float distanceToPlayer)
        {
            if (distanceToPlayer > attackRange)
            {
                // Chase player
                if (agent != null && agent.isOnNavMesh)
                {
                    agent.SetDestination(player.position);
                }
            }
            else
            {
                // Attack player
                if (agent != null)
                {
                    agent.SetDestination(transform.position);
                }
                
                LookAtPlayer();
                TryAttack();
            }
        }

        private void DefensiveBehavior(float distanceToPlayer)
        {
            float idealDistance = attackRange * 1.5f;
            
            if (distanceToPlayer < idealDistance)
            {
                // Move away from player
                Vector3 directionAway = transform.position - player.position;
                Vector3 newPos = transform.position + directionAway.normalized * 2f;
                
                if (agent != null && agent.isOnNavMesh)
                {
                    agent.SetDestination(newPos);
                }
            }
            else if (distanceToPlayer <= attackRange * 2f)
            {
                // In attack range
                if (agent != null)
                {
                    agent.SetDestination(transform.position);
                }
                
                LookAtPlayer();
                TryAttack();
            }
        }

        private void FlyingBehavior(float distanceToPlayer)
        {
            // Flying enemies hover and swoop
            if (distanceToPlayer > attackRange * 2f)
            {
                // Fly towards player
                Vector3 targetPos = player.position + Vector3.up * 3f;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            }
            else
            {
                // Circle and attack
                LookAtPlayer();
                TryAttack();
            }
        }

        private void SupportBehavior(float distanceToPlayer)
        {
            // Support enemies maintain distance and use abilities
            float supportDistance = attackRange * 3f;
            
            if (distanceToPlayer < supportDistance)
            {
                // Move to support distance
                Vector3 directionAway = transform.position - player.position;
                Vector3 newPos = transform.position + directionAway.normalized * 2f;
                
                if (agent != null && agent.isOnNavMesh)
                {
                    agent.SetDestination(newPos);
                }
            }
            
            LookAtPlayer();
            TryAttack(); // "Attack" would be support abilities
        }

        private void ExecuteIdleBehavior()
        {
            if (behaviorType == AIBehaviorType.Patrol && patrolPoints.Length > 0)
            {
                Patrol();
            }
            else if (behaviorType == AIBehaviorType.Stationary)
            {
                // Do nothing, just idle
            }
        }

        private void Patrol()
        {
            if (isWaiting)
            {
                patrolWaitTimer -= Time.deltaTime;
                if (patrolWaitTimer <= 0)
                {
                    isWaiting = false;
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                }
                return;
            }

            if (agent != null && agent.isOnNavMesh)
            {
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
                {
                    isWaiting = true;
                    patrolWaitTimer = patrolWaitTime;
                }
            }
        }

        private void TryAttack()
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                PerformAttack();
            }
        }

        private void PerformAttack()
        {
            Debug.Log($"{health.enemyType} attacks!");
            
            // Check if player is in range
            if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                var playerStats = player.GetComponent<Player.PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamage);
                }
            }
        }

        private void LookAtPlayer()
        {
            if (player != null)
            {
                Vector3 direction = player.position - transform.position;
                direction.y = 0;
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
                }
            }
        }

        public void OnTakeDamage()
        {
            // React to taking damage
            if (!hasDetectedPlayer)
            {
                hasDetectedPlayer = true;
            }
        }

        public void OnDeath()
        {
            // Death behavior
            if (agent != null)
            {
                agent.enabled = false;
            }
        }

        private void OnPlayerDetected()
        {
            Debug.Log($"{health.enemyType} detected player!");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
