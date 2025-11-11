using UnityEngine;
using System.Collections.Generic;

namespace IslasApocalipsis.Magic
{
    /// <summary>
    /// Manages player's magic system including spell casting and mana
    /// </summary>
    public class MagicSystem : MonoBehaviour
    {
        [Header("Equipped Spells")]
        public SpellData[] equippedSpells = new SpellData[8]; // 8 quick slots
        
        [Header("Settings")]
        public Transform castPoint;
        public LayerMask enemyLayer;
        
        private Player.PlayerStats stats;
        private Dictionary<SpellData, float> spellCooldowns = new Dictionary<SpellData, float>();
        private bool isCasting = false;
        private float castStartTime;
        private SpellData currentlyCastingSpell;

        private void Awake()
        {
            stats = GetComponent<Player.PlayerStats>();
        }

        private void Update()
        {
            HandleSpellInput();
            UpdateCooldowns();
            UpdateCasting();
        }

        private void HandleSpellInput()
        {
            if (isCasting) return;

            // Check number keys 1-8 for quick cast
            for (int i = 0; i < equippedSpells.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) && equippedSpells[i] != null)
                {
                    CastSpell(equippedSpells[i]);
                    break;
                }
            }

            // Alternative: Mouse button for main spell
            if (Input.GetMouseButtonDown(1) && equippedSpells[0] != null) // Right mouse button
            {
                CastSpell(equippedSpells[0]);
            }
        }

        public bool CastSpell(SpellData spell)
        {
            if (spell == null) return false;
            
            // Check mana
            if (stats.currentMana < spell.manaCost)
            {
                Debug.Log("Not enough mana!");
                return false;
            }

            // Check cooldown
            if (IsOnCooldown(spell))
            {
                Debug.Log($"{spell.spellName} is on cooldown!");
                return false;
            }

            // Start casting
            isCasting = true;
            castStartTime = Time.time;
            currentlyCastingSpell = spell;
            
            Debug.Log($"Casting {spell.spellName}...");
            
            // Instant cast if cast time is 0
            if (spell.castTime <= 0)
            {
                CompleteCast();
            }

            return true;
        }

        private void UpdateCasting()
        {
            if (isCasting && currentlyCastingSpell != null)
            {
                if (Time.time >= castStartTime + currentlyCastingSpell.castTime)
                {
                    CompleteCast();
                }
            }
        }

        private void CompleteCast()
        {
            if (currentlyCastingSpell == null) return;

            // Consume mana
            stats.ConsumeMana(currentlyCastingSpell.manaCost);

            // Execute spell effect
            ExecuteSpell(currentlyCastingSpell);

            // Set cooldown
            spellCooldowns[currentlyCastingSpell] = Time.time + currentlyCastingSpell.cooldown;

            Debug.Log($"Cast complete: {currentlyCastingSpell.spellName}");

            // Reset casting state
            isCasting = false;
            currentlyCastingSpell = null;
        }

        private void ExecuteSpell(SpellData spell)
        {
            Vector3 spawnPos = castPoint != null ? castPoint.position : transform.position + transform.forward + Vector3.up;

            switch (spell.spellType)
            {
                case SpellType.Projectile:
                    CastProjectile(spell, spawnPos);
                    break;
                case SpellType.AreaOfEffect:
                    CastAoE(spell, spawnPos);
                    break;
                case SpellType.Buff:
                    CastBuff(spell);
                    break;
                case SpellType.Debuff:
                    CastDebuff(spell);
                    break;
                case SpellType.Summon:
                    CastSummon(spell, spawnPos);
                    break;
                case SpellType.Environmental:
                    CastEnvironmental(spell, spawnPos);
                    break;
            }
        }

        private void CastProjectile(SpellData spell, Vector3 position)
        {
            if (spell.spellEffectPrefab != null)
            {
                GameObject projectile = Instantiate(spell.spellEffectPrefab, position, transform.rotation);
                
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = transform.forward * spell.projectileSpeed;
                }

                // Add projectile script
                var projScript = projectile.GetComponent<Projectile>();
                if (projScript == null)
                {
                    projScript = projectile.AddComponent<Projectile>();
                }
                projScript.Initialize(spell.damage, spell.element, spell.range);

                Destroy(projectile, 5f); // Cleanup after 5 seconds
            }
        }

        private void CastAoE(SpellData spell, Vector3 position)
        {
            // Create AoE effect
            if (spell.spellEffectPrefab != null)
            {
                GameObject aoe = Instantiate(spell.spellEffectPrefab, position, Quaternion.identity);
                Destroy(aoe, spell.duration);
            }

            // Deal damage in radius
            Collider[] hits = Physics.OverlapSphere(position, spell.aoeRadius, enemyLayer);
            foreach (Collider hit in hits)
            {
                var enemy = hit.GetComponent<Enemies.EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(spell.damage);
                    ApplySpellEffects(enemy, spell);
                }
            }
        }

        private void CastBuff(SpellData spell)
        {
            Debug.Log($"Applied buff: {spell.spellName}");
            // Apply buff to player
            // This would integrate with a buff system
        }

        private void CastDebuff(SpellData spell)
        {
            Debug.Log($"Applied debuff to target: {spell.spellName}");
            // Apply debuff to target
        }

        private void CastSummon(SpellData spell, Vector3 position)
        {
            if (spell.spellEffectPrefab != null)
            {
                GameObject summon = Instantiate(spell.spellEffectPrefab, position, Quaternion.identity);
                Destroy(summon, spell.duration);
                Debug.Log($"Summoned: {spell.spellName}");
            }
        }

        private void CastEnvironmental(SpellData spell, Vector3 position)
        {
            Debug.Log($"Environmental effect: {spell.spellName}");
            // Environmental interaction (e.g., freeze water, light torch, etc.)
        }

        private void ApplySpellEffects(Enemies.EnemyHealth enemy, SpellData spell)
        {
            // Apply status effects based on spell
            if (spell.appliesBurn)
            {
                // Apply burn effect
            }
            if (spell.appliesFreeze)
            {
                // Apply freeze effect
            }
            if (spell.appliesStun)
            {
                // Apply stun effect
            }
            if (spell.appliesSlow)
            {
                // Apply slow effect
            }
        }

        private void UpdateCooldowns()
        {
            // Cooldowns are checked in real-time, no update needed
        }

        private bool IsOnCooldown(SpellData spell)
        {
            if (spellCooldowns.ContainsKey(spell))
            {
                return Time.time < spellCooldowns[spell];
            }
            return false;
        }

        public float GetCooldownRemaining(SpellData spell)
        {
            if (spellCooldowns.ContainsKey(spell))
            {
                float remaining = spellCooldowns[spell] - Time.time;
                return Mathf.Max(0, remaining);
            }
            return 0f;
        }

        public void EquipSpell(SpellData spell, int slot)
        {
            if (slot >= 0 && slot < equippedSpells.Length)
            {
                equippedSpells[slot] = spell;
                Debug.Log($"Equipped {spell.spellName} to slot {slot + 1}");
            }
        }
    }

    /// <summary>
    /// Projectile behavior for spell projectiles
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        private float damage;
        private Items.ElementType element;
        private float maxDistance;
        private Vector3 startPosition;

        public void Initialize(float dmg, Items.ElementType elem, float distance)
        {
            damage = dmg;
            element = elem;
            maxDistance = distance;
            startPosition = transform.position;
        }

        private void Update()
        {
            if (Vector3.Distance(startPosition, transform.position) > maxDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemies.EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
