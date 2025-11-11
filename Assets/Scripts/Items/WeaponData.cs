using UnityEngine;

namespace IslasApocalipsis.Items
{
    /// <summary>
    /// Weapon types available in the game
    /// </summary>
    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Axe,
        Hammer,
        Spear,
        Lance,
        Bow,
        Crossbow,
        Dagger,
        Knife,
        Staff,
        TechGun
    }

    /// <summary>
    /// Elemental types for weapons and magic
    /// </summary>
    public enum ElementType
    {
        None,
        Fire,
        Ice,
        Lightning,
        Earth,
        Wind,
        Shadow,
        Light,
        Time
    }

    /// <summary>
    /// ScriptableObject for weapon data
    /// </summary>
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Islas Apocalipsis/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [Header("Basic Info")]
        public string weaponName = "Weapon";
        public WeaponType weaponType;
        public Sprite icon;
        public GameObject weaponPrefab;

        [Header("Stats")]
        public float baseDamage = 10f;
        public float attackSpeed = 1f;
        public float range = 2f;
        public int maxDurability = 100;
        
        [Header("Special Properties")]
        public ElementType elementType = ElementType.None;
        public float elementalDamage = 0f;
        public bool isTwoHanded = false;
        public int upgradeSlots = 3;

        [Header("Requirements")]
        public int requiredLevel = 1;
        public float requiredStrength = 0f;
        public float requiredDexterity = 0f;

        [TextArea(3, 5)]
        public string description = "A weapon forged in the apocalypse.";
    }
}
