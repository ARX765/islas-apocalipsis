using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IslasApocalipsis.UI
{
    /// <summary>
    /// Main UI manager for HUD and menus
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [Header("HUD Elements")]
        public Slider healthBar;
        public Slider staminaBar;
        public Slider manaBar;
        public Slider weaponDurabilityBar;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI staminaText;
        public TextMeshProUGUI manaText;
        public TextMeshProUGUI comboCounterText;

        [Header("Menus")]
        public GameObject pauseMenu;
        public GameObject inventoryMenu;
        public GameObject mapMenu;
        public GameObject settingsMenu;

        [Header("Boss UI")]
        public GameObject bossHealthBarContainer;
        public Slider bossHealthBar;
        public TextMeshProUGUI bossNameText;

        [Header("Notifications")]
        public GameObject notificationPanel;
        public TextMeshProUGUI notificationText;

        private Player.PlayerStats playerStats;
        private Combat.CombatSystem combatSystem;
        private Combat.WeaponManager weaponManager;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Find player components
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerStats = player.GetComponent<Player.PlayerStats>();
                combatSystem = player.GetComponent<Combat.CombatSystem>();
                weaponManager = player.GetComponent<Combat.WeaponManager>();
            }

            // Hide menus initially
            if (pauseMenu != null) pauseMenu.SetActive(false);
            if (inventoryMenu != null) inventoryMenu.SetActive(false);
            if (mapMenu != null) mapMenu.SetActive(false);
            if (settingsMenu != null) settingsMenu.SetActive(false);
            if (bossHealthBarContainer != null) bossHealthBarContainer.SetActive(false);
        }

        private void Update()
        {
            UpdateHUD();
            HandleMenuInputs();
        }

        private void UpdateHUD()
        {
            if (playerStats == null) return;

            // Update health
            if (healthBar != null)
            {
                healthBar.value = playerStats.GetHealthPercent();
            }
            if (healthText != null)
            {
                healthText.text = $"{(int)playerStats.currentHealth}/{(int)playerStats.maxHealth}";
            }

            // Update stamina
            if (staminaBar != null)
            {
                staminaBar.value = playerStats.GetStaminaPercent();
            }
            if (staminaText != null)
            {
                staminaText.text = $"{(int)playerStats.currentStamina}/{(int)playerStats.maxStamina}";
            }

            // Update mana
            if (manaBar != null)
            {
                manaBar.value = playerStats.GetManaPercent();
            }
            if (manaText != null)
            {
                manaText.text = $"{(int)playerStats.currentMana}/{(int)playerStats.maxMana}";
            }

            // Update weapon durability
            if (weaponDurabilityBar != null && weaponManager != null)
            {
                weaponDurabilityBar.value = weaponManager.GetDurabilityPercent();
            }

            // Update combo counter
            if (comboCounterText != null && combatSystem != null)
            {
                int combo = combatSystem.GetComboCount();
                if (combo > 0)
                {
                    comboCounterText.gameObject.SetActive(true);
                    comboCounterText.text = $"COMBO x{combo}";
                }
                else
                {
                    comboCounterText.gameObject.SetActive(false);
                }
            }
        }

        private void HandleMenuInputs()
        {
            // Inventory
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }

            // Map
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMap();
            }
        }

        public void ShowPauseMenu()
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
        }

        public void HidePauseMenu()
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        }

        public void ToggleInventory()
        {
            if (inventoryMenu != null)
            {
                bool isActive = inventoryMenu.activeSelf;
                inventoryMenu.SetActive(!isActive);
                Time.timeScale = isActive ? 1f : 0f;
            }
        }

        public void ToggleMap()
        {
            if (mapMenu != null)
            {
                bool isActive = mapMenu.activeSelf;
                mapMenu.SetActive(!isActive);
                Time.timeScale = isActive ? 1f : 0f;
            }
        }

        public void ShowBossHealthBar(string bossName, float healthPercent)
        {
            if (bossHealthBarContainer != null)
            {
                bossHealthBarContainer.SetActive(true);
            }
            if (bossNameText != null)
            {
                bossNameText.text = bossName;
            }
            if (bossHealthBar != null)
            {
                bossHealthBar.value = healthPercent;
            }
        }

        public void UpdateBossHealthBar(float healthPercent)
        {
            if (bossHealthBar != null)
            {
                bossHealthBar.value = healthPercent;
            }
        }

        public void HideBossHealthBar()
        {
            if (bossHealthBarContainer != null)
            {
                bossHealthBarContainer.SetActive(false);
            }
        }

        public void ShowNotification(string message, float duration = 3f)
        {
            if (notificationPanel != null && notificationText != null)
            {
                notificationPanel.SetActive(true);
                notificationText.text = message;
                Invoke(nameof(HideNotification), duration);
            }
        }

        private void HideNotification()
        {
            if (notificationPanel != null)
            {
                notificationPanel.SetActive(false);
            }
        }

        // Button callbacks
        public void OnResumeButton()
        {
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.TogglePause();
            }
        }

        public void OnSaveButton()
        {
            if (Systems.SaveSystem.Instance != null)
            {
                Systems.SaveSystem.Instance.QuickSave();
                ShowNotification("Game Saved");
            }
        }

        public void OnLoadButton()
        {
            if (Systems.SaveSystem.Instance != null)
            {
                Systems.SaveSystem.Instance.QuickLoad();
                ShowNotification("Game Loaded");
            }
        }

        public void OnQuitButton()
        {
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.QuitGame();
            }
        }
    }
}
