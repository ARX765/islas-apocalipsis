using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace IslasApocalipsis.Systems
{
    /// <summary>
    /// Game save data structure
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        public string saveName;
        public DateTime saveTime;
        public float playTime;

        // Player data
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public float currentHealth;
        public float currentStamina;
        public float currentMana;
        public int playerLevel;

        // Progress
        public List<string> defeatedBosses = new List<string>();
        public List<string> discoveredLocations = new List<string>();
        public List<string> completedQuests = new List<string>();
        
        // Inventory
        public List<string> ownedWeapons = new List<string>();
        public List<string> learnedSpells = new List<string>();
        public string equippedWeapon;
        
        // World state
        public string currentScene;
        public Dictionary<string, bool> worldFlags = new Dictionary<string, bool>();
    }

    /// <summary>
    /// Manages game saving and loading
    /// </summary>
    public class SaveSystem : MonoBehaviour
    {
        private static SaveSystem _instance;
        public static SaveSystem Instance => _instance;

        [Header("Save Settings")]
        public int maxSaveSlots = 3;
        public bool autoSaveEnabled = true;
        public float autoSaveInterval = 300f; // 5 minutes

        private string saveDirectory;
        private float autoSaveTimer = 0f;
        private SaveData currentSave;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSaveSystem();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSaveSystem()
        {
            saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            Debug.Log($"Save directory: {saveDirectory}");
        }

        private void Update()
        {
            if (autoSaveEnabled)
            {
                autoSaveTimer += Time.deltaTime;
                if (autoSaveTimer >= autoSaveInterval)
                {
                    AutoSave();
                    autoSaveTimer = 0f;
                }
            }
        }

        public void SaveGame(int slot)
        {
            if (slot < 0 || slot >= maxSaveSlots)
            {
                Debug.LogError("Invalid save slot!");
                return;
            }

            SaveData data = GatherSaveData();
            data.saveName = $"Save {slot + 1}";
            data.saveTime = DateTime.Now;

            string json = JsonUtility.ToJson(data, true);
            string filePath = GetSaveFilePath(slot);

            try
            {
                File.WriteAllText(filePath, json);
                currentSave = data;
                Debug.Log($"Game saved to slot {slot + 1}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save game: {e.Message}");
            }
        }

        public bool LoadGame(int slot)
        {
            if (slot < 0 || slot >= maxSaveSlots)
            {
                Debug.LogError("Invalid save slot!");
                return false;
            }

            string filePath = GetSaveFilePath(slot);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"No save file found in slot {slot + 1}");
                return false;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                ApplySaveData(data);
                currentSave = data;
                Debug.Log($"Game loaded from slot {slot + 1}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load game: {e.Message}");
                return false;
            }
        }

        public void DeleteSave(int slot)
        {
            if (slot < 0 || slot >= maxSaveSlots)
            {
                Debug.LogError("Invalid save slot!");
                return;
            }

            string filePath = GetSaveFilePath(slot);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Save slot {slot + 1} deleted");
            }
        }

        public bool SaveExists(int slot)
        {
            if (slot < 0 || slot >= maxSaveSlots) return false;
            return File.Exists(GetSaveFilePath(slot));
        }

        public SaveData GetSaveInfo(int slot)
        {
            if (!SaveExists(slot)) return null;

            try
            {
                string json = File.ReadAllText(GetSaveFilePath(slot));
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch
            {
                return null;
            }
        }

        private void AutoSave()
        {
            // Auto-save to a special slot
            SaveGame(0); // Use slot 0 for auto-save
            Debug.Log("Auto-save complete");
        }

        private SaveData GatherSaveData()
        {
            SaveData data = new SaveData();

            // Find player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                data.playerPosition = player.transform.position;
                data.playerRotation = player.transform.rotation;

                var stats = player.GetComponent<Player.PlayerStats>();
                if (stats != null)
                {
                    data.currentHealth = stats.currentHealth;
                    data.currentStamina = stats.currentStamina;
                    data.currentMana = stats.currentMana;
                    data.playerLevel = stats.level;
                }
            }

            // Gather play time from GameManager
            if (Core.GameManager.Instance != null)
            {
                data.playTime = Core.GameManager.Instance.gameTime;
            }

            // Current scene
            data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            return data;
        }

        private void ApplySaveData(SaveData data)
        {
            // Load scene if different
            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (currentScene != data.currentScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(data.currentScene);
            }

            // Apply player data
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = data.playerPosition;
                player.transform.rotation = data.playerRotation;

                var stats = player.GetComponent<Player.PlayerStats>();
                if (stats != null)
                {
                    stats.currentHealth = data.currentHealth;
                    stats.currentStamina = data.currentStamina;
                    stats.currentMana = data.currentMana;
                    stats.level = data.playerLevel;
                }
            }

            // Apply play time
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.Instance.gameTime = data.playTime;
            }
        }

        private string GetSaveFilePath(int slot)
        {
            return Path.Combine(saveDirectory, $"save_{slot}.json");
        }

        public void QuickSave()
        {
            SaveGame(0);
            Debug.Log("Quick save complete");
        }

        public void QuickLoad()
        {
            if (LoadGame(0))
            {
                Debug.Log("Quick load complete");
            }
        }
    }
}
