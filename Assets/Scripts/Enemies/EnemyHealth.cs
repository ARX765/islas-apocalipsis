using UnityEngine;

namespace IslasApocalipsis.Enemies
{
    /// <summary>
    /// Enemy types in the game
    /// </summary>
    public enum EnemyType
    {
        // Common Enemies
        CorruptedScout,
        SkyRaider,
        TechDrone,
        VoidCrawler,
        StoneGolem,
        CrystalSpider,
        ShadowWraith,
        FlameImp,
        IceSprite,
        LightningWisp,
        PoisonBloat,
        RustySentinel,
        WildBeast,
        CorruptedMage,
        TechSoldier,
        BladeDancer,
        SiegeBreaker,
        SkySniper,
        VoidHound,
        CrystalConstruct,
        
        // Elite Enemies
        AncientGuardian,
        SkyCaptain,
        TechOverseer,
        VoidTitan,
        FrostGiant,
        StormCaller,
        ShadowAssassin,
        FireDemon,
        CrystalWyrm,
        CorruptedChampion,
        
        // Special Enemies
        MimicChest,
        ExplosiveCarrier,
        HealerPriest,
        SummonerCultist,
        BerserkerBrute,
        ShieldedKnight,
        DualBladeRonin,
        ElementalCore,
        Necromancer,
        TimeWarper
    }

    /// <summary>
    /// Base enemy health and damage system
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Stats")]
        public EnemyType enemyType;
        public float maxHealth = 50f;
        public float currentHealth;
        public float defense = 0f;
        public bool isElite = false;
        public bool isBoss = false;

        [Header("Resistances")]
        public float fireResistance = 0f;
        public float iceResistance = 0f;
        public float lightningResistance = 0f;
        public float physicalResistance = 0f;

        private EnemyAI enemyAI;
        private bool isDead = false;

        private void Awake()
        {
            currentHealth = maxHealth;
            enemyAI = GetComponent<EnemyAI>();
        }

        public void TakeDamage(float damage, Items.ElementType element = Items.ElementType.None)
        {
            if (isDead) return;

            // Apply resistances
            float resistance = 0f;
            switch (element)
            {
                case Items.ElementType.Fire:
                    resistance = fireResistance;
                    break;
                case Items.ElementType.Ice:
                    resistance = iceResistance;
                    break;
                case Items.ElementType.Lightning:
                    resistance = lightningResistance;
                    break;
                default:
                    resistance = physicalResistance;
                    break;
            }

            // Calculate actual damage
            float actualDamage = damage * (1f - resistance) - defense;
            actualDamage = Mathf.Max(actualDamage, 1f); // Minimum 1 damage

            currentHealth -= actualDamage;
            
            Debug.Log($"{enemyType} took {actualDamage} damage. Health: {currentHealth}/{maxHealth}");

            // Notify AI of damage
            if (enemyAI != null)
            {
                enemyAI.OnTakeDamage();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            isDead = true;
            Debug.Log($"{enemyType} defeated!");

            // Drop loot, give experience, etc.
            if (enemyAI != null)
            {
                enemyAI.OnDeath();
            }

            // Destroy after animation
            Destroy(gameObject, 2f);
        }

        public float GetHealthPercent() => currentHealth / maxHealth;
        public bool IsDead() => isDead;
    }
}
