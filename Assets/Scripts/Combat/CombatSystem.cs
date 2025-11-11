using UnityEngine;
using System.Collections.Generic;

namespace IslasApocalipsis.Combat
{
    /// <summary>
    /// Handles player combat including attacks, combos, and parrying
    /// </summary>
    public class CombatSystem : MonoBehaviour
    {
        [Header("Combat Settings")]
        public float lightAttackDamage = 10f;
        public float heavyAttackDamage = 25f;
        public float attackRange = 2f;
        public float attackCooldown = 0.5f;
        public float heavyAttackCooldown = 1.5f;
        public int maxComboCount = 3;

        [Header("Parry Settings")]
        public float parryWindow = 0.3f;
        public float perfectDodgeWindow = 0.2f;
        public float perfectDodgeSlowMotion = 0.3f;
        public float slowMotionDuration = 2f;

        [Header("Target Lock")]
        public bool isLockedOn = false;
        public float lockOnRange = 15f;
        public LayerMask enemyLayer;

        [Header("References")]
        public Transform attackPoint;
        
        private PlayerStats stats;
        private WeaponManager weaponManager;
        
        private float lastAttackTime;
        private int comboCounter = 0;
        private float comboResetTime = 1f;
        private float lastComboTime;
        
        private bool isAttacking = false;
        private bool isParrying = false;
        private float parryStartTime;
        
        private Transform currentTarget;

        private void Awake()
        {
            stats = GetComponent<PlayerStats>();
            weaponManager = GetComponent<WeaponManager>();
        }

        private void Update()
        {
            HandleCombatInput();
            UpdateCombo();
            HandleTargetLock();
        }

        private void HandleCombatInput()
        {
            // Light attack
            if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown && !isAttacking)
            {
                PerformLightAttack();
            }

            // Heavy attack
            if (Input.GetButtonDown("Fire2") && Time.time >= lastAttackTime + heavyAttackCooldown && !isAttacking)
            {
                PerformHeavyAttack();
            }

            // Parry
            if (Input.GetKeyDown(KeyCode.Q) && !isParrying)
            {
                StartParry();
            }

            // Toggle lock-on
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleLockOn();
            }
        }

        private void PerformLightAttack()
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            lastComboTime = Time.time;

            comboCounter++;
            if (comboCounter > maxComboCount)
            {
                comboCounter = 1;
            }

            float finalDamage = lightAttackDamage + stats.attackPower;
            
            // Apply combo multiplier
            float comboMultiplier = 1f + (comboCounter - 1) * 0.2f;
            finalDamage *= comboMultiplier;

            Debug.Log($"Light Attack! Combo: {comboCounter}/{maxComboCount}, Damage: {finalDamage}");

            DealDamageInRange(finalDamage);
            
            // Animation would trigger here
            Invoke(nameof(ResetAttacking), 0.3f);
        }

        private void PerformHeavyAttack()
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            lastComboTime = Time.time;

            comboCounter = 0; // Heavy attack resets combo

            float finalDamage = heavyAttackDamage + stats.attackPower * 1.5f;
            
            Debug.Log($"Heavy Attack! Damage: {finalDamage}");

            DealDamageInRange(finalDamage);
            
            // Animation would trigger here
            Invoke(nameof(ResetAttacking), 0.6f);
        }

        private void DealDamageInRange(float damage)
        {
            Vector3 attackPosition = attackPoint != null ? attackPoint.position : transform.position + transform.forward;
            
            Collider[] hitEnemies = Physics.OverlapSphere(attackPosition, attackRange, enemyLayer);
            
            foreach (Collider enemy in hitEnemies)
            {
                var enemyHealth = enemy.GetComponent<IslasApocalipsis.Enemies.EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }

        private void StartParry()
        {
            isParrying = true;
            parryStartTime = Time.time;
            Debug.Log("Parry started");
            
            Invoke(nameof(EndParry), parryWindow);
        }

        private void EndParry()
        {
            isParrying = false;
            Debug.Log("Parry ended");
        }

        public bool AttemptParry()
        {
            if (isParrying && Time.time <= parryStartTime + parryWindow)
            {
                Debug.Log("Successful parry!");
                return true;
            }
            return false;
        }

        public bool AttemptPerfectDodge()
        {
            // Check if dodge was timed perfectly (would be called from dodge action)
            // Triggers slow motion effect
            Time.timeScale = perfectDodgeSlowMotion;
            Invoke(nameof(ResetTimeScale), slowMotionDuration);
            Debug.Log("Perfect dodge! Slow motion activated");
            return true;
        }

        private void ResetTimeScale()
        {
            Time.timeScale = 1f;
        }

        private void UpdateCombo()
        {
            if (Time.time - lastComboTime > comboResetTime && comboCounter > 0)
            {
                comboCounter = 0;
                Debug.Log("Combo reset");
            }
        }

        private void ToggleLockOn()
        {
            if (!isLockedOn)
            {
                FindNearestEnemy();
            }
            else
            {
                currentTarget = null;
                isLockedOn = false;
            }
        }

        private void FindNearestEnemy()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRange, enemyLayer);
            
            if (enemies.Length > 0)
            {
                Transform nearest = null;
                float nearestDistance = lockOnRange;

                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearest = enemy.transform;
                        nearestDistance = distance;
                    }
                }

                if (nearest != null)
                {
                    currentTarget = nearest;
                    isLockedOn = true;
                    Debug.Log($"Locked onto: {currentTarget.name}");
                }
            }
        }

        private void HandleTargetLock()
        {
            if (isLockedOn && currentTarget != null)
            {
                // Face the target
                Vector3 directionToTarget = currentTarget.position - transform.position;
                directionToTarget.y = 0;
                
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                // Check if target is still in range
                if (Vector3.Distance(transform.position, currentTarget.position) > lockOnRange)
                {
                    currentTarget = null;
                    isLockedOn = false;
                    Debug.Log("Target lost");
                }
            }
        }

        private void ResetAttacking()
        {
            isAttacking = false;
        }

        public int GetComboCount() => comboCounter;
        public bool IsAttacking() => isAttacking;
        public Transform GetCurrentTarget() => currentTarget;

        private void OnDrawGizmosSelected()
        {
            Vector3 attackPosition = attackPoint != null ? attackPoint.position : transform.position + transform.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition, attackRange);
            
            if (isLockedOn)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, lockOnRange);
            }
        }
    }
}
