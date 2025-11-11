using UnityEngine;

namespace IslasApocalipsis.Magic
{
    /// <summary>
    /// Spell types for different magic schools
    /// </summary>
    public enum SpellType
    {
        Projectile,
        AreaOfEffect,
        Buff,
        Debuff,
        Summon,
        Environmental
    }

    /// <summary>
    /// ScriptableObject for spell data
    /// </summary>
    [CreateAssetMenu(fileName = "New Spell", menuName = "Islas Apocalipsis/Spell")]
    public class SpellData : ScriptableObject
    {
        [Header("Basic Info")]
        public string spellName = "Spell";
        public Items.ElementType element;
        public SpellType spellType;
        public Sprite icon;
        public GameObject spellEffectPrefab;

        [Header("Stats")]
        public float manaCost = 20f;
        public float damage = 15f;
        public float castTime = 0.5f;
        public float cooldown = 2f;
        public float range = 10f;
        public float aoeRadius = 0f;
        public float duration = 5f;

        [Header("Projectile Settings")]
        public float projectileSpeed = 20f;
        public bool homingProjectile = false;

        [Header("Effects")]
        public bool appliesBurn = false;
        public bool appliesFreeze = false;
        public bool appliesStun = false;
        public bool appliesSlow = false;
        public float effectDuration = 3f;

        [Header("Requirements")]
        public int requiredLevel = 1;
        public float requiredIntelligence = 0f;

        [TextArea(3, 5)]
        public string description = "A spell imbued with magical energy.";
    }
}
