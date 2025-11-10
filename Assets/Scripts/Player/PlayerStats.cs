using UnityEngine;

namespace IslasApocalipsis.Player
{
    /// <summary>
    /// Manages player stats including health, stamina, and mana
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        [Header("Health")]
        public float maxHealth = 100f;
        public float currentHealth;
        public float healthRegenRate = 5f;
        public float healthRegenDelay = 5f;
        private float lastDamageTime;

        [Header("Stamina")]
        public float maxStamina = 100f;
        public float currentStamina;
        public float staminaRegenRate = 20f;
        public float staminaRegenDelay = 1f;
        public float sprintStaminaCost = 10f;
        public float jumpStaminaCost = 15f;
        public float climbStaminaCost = 10f;
        private float lastStaminaUseTime;

        [Header("Mana")]
        public float maxMana = 100f;
        public float currentMana;
        public float manaRegenRate = 10f;
        public float manaRegenDelay = 2f;
        private float lastManaUseTime;

        [Header("Stats")]
        public int level = 1;
        public float attackPower = 10f;
        public float defense = 5f;

        private void Awake()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            currentMana = maxMana;
        }

        private void Update()
        {
            RegenerateStats();
        }

        private void RegenerateStats()
        {
            // Health regeneration
            if (Time.time - lastDamageTime >= healthRegenDelay && currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(currentHealth + healthRegenRate * Time.deltaTime, maxHealth);
            }

            // Stamina regeneration
            if (Time.time - lastStaminaUseTime >= staminaRegenDelay && currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(currentStamina + staminaRegenRate * Time.deltaTime, maxStamina);
            }

            // Mana regeneration
            if (Time.time - lastManaUseTime >= manaRegenDelay && currentMana < maxMana)
            {
                currentMana = Mathf.Min(currentMana + manaRegenRate * Time.deltaTime, maxMana);
            }
        }

        public void TakeDamage(float damage)
        {
            float actualDamage = Mathf.Max(damage - defense, 1f);
            currentHealth = Mathf.Max(currentHealth - actualDamage, 0f);
            lastDamageTime = Time.time;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }

        public bool ConsumeStamina(float amount)
        {
            if (currentStamina >= amount)
            {
                currentStamina -= amount;
                lastStaminaUseTime = Time.time;
                return true;
            }
            return false;
        }

        public bool ConsumeMana(float amount)
        {
            if (currentMana >= amount)
            {
                currentMana -= amount;
                lastManaUseTime = Time.time;
                return true;
            }
            return false;
        }

        public bool CanSprint()
        {
            return currentStamina > sprintStaminaCost * Time.deltaTime;
        }

        public bool CanJump()
        {
            return currentStamina >= jumpStaminaCost;
        }

        public bool CanClimb()
        {
            return currentStamina > climbStaminaCost * Time.deltaTime;
        }

        private void Die()
        {
            Debug.Log("Player died!");
            // Trigger death event/respawn logic
        }

        // Getters for UI
        public float GetHealthPercent() => currentHealth / maxHealth;
        public float GetStaminaPercent() => currentStamina / maxStamina;
        public float GetManaPercent() => currentMana / maxMana;
    }
}
