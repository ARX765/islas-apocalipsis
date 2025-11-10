using UnityEngine;
using System.Collections.Generic;

namespace IslasApocalipsis.Combat
{
    /// <summary>
    /// Manages player's equipped weapons and weapon switching
    /// </summary>
    public class WeaponManager : MonoBehaviour
    {
        [Header("Current Weapons")]
        public Items.WeaponData currentWeapon;
        public Items.WeaponData secondaryWeapon;
        
        [Header("Weapon Slots")]
        public Transform rightHandSlot;
        public Transform leftHandSlot;
        public Transform backSlot;

        private GameObject currentWeaponObject;
        private GameObject secondaryWeaponObject;
        private int currentDurability;
        private List<Items.WeaponData> inventory = new List<Items.WeaponData>();

        private void Start()
        {
            if (currentWeapon != null)
            {
                EquipWeapon(currentWeapon);
            }
        }

        private void Update()
        {
            // Weapon switching
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchToWeaponSlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToWeaponSlot(1);
            }
        }

        public void EquipWeapon(Items.WeaponData weapon)
        {
            if (weapon == null) return;

            // Destroy old weapon
            if (currentWeaponObject != null)
            {
                Destroy(currentWeaponObject);
            }

            currentWeapon = weapon;
            currentDurability = weapon.maxDurability;

            // Instantiate new weapon
            if (weapon.weaponPrefab != null)
            {
                Transform slot = weapon.isTwoHanded ? rightHandSlot : rightHandSlot;
                currentWeaponObject = Instantiate(weapon.weaponPrefab, slot);
                currentWeaponObject.transform.localPosition = Vector3.zero;
                currentWeaponObject.transform.localRotation = Quaternion.identity;
            }

            Debug.Log($"Equipped: {weapon.weaponName}");
        }

        public void SwitchToWeaponSlot(int slot)
        {
            if (slot == 0 && currentWeapon != null)
            {
                EquipWeapon(currentWeapon);
            }
            else if (slot == 1 && secondaryWeapon != null)
            {
                EquipWeapon(secondaryWeapon);
            }
        }

        public void AddWeaponToInventory(Items.WeaponData weapon)
        {
            if (!inventory.Contains(weapon))
            {
                inventory.Add(weapon);
                Debug.Log($"Added to inventory: {weapon.weaponName}");
            }
        }

        public float GetCurrentDamage()
        {
            if (currentWeapon == null) return 0f;
            
            float durabilityMultiplier = (float)currentDurability / currentWeapon.maxDurability;
            return currentWeapon.baseDamage * durabilityMultiplier;
        }

        public void DamageWeapon(int amount = 1)
        {
            currentDurability = Mathf.Max(0, currentDurability - amount);
            
            if (currentDurability <= 0)
            {
                Debug.Log($"{currentWeapon.weaponName} has broken!");
                // Weapon breaks logic
            }
        }

        public void RepairWeapon(int amount)
        {
            if (currentWeapon != null)
            {
                currentDurability = Mathf.Min(currentWeapon.maxDurability, currentDurability + amount);
            }
        }

        public float GetDurabilityPercent()
        {
            if (currentWeapon == null) return 0f;
            return (float)currentDurability / currentWeapon.maxDurability;
        }
    }
}
