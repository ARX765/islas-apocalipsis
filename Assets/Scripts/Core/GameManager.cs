using UnityEngine;

namespace IslasApocalipsis.Core
{
    /// <summary>
    /// Core game manager that persists across scenes and manages global game state
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }

        [Header("Game Settings")]
        public float targetFrameRate = 60f;
        public bool showDebugInfo = false;

        [Header("Game State")]
        public bool isPaused = false;
        public float gameTime = 0f;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGame();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializeGame()
        {
            Application.targetFrameRate = (int)targetFrameRate;
            QualitySettings.vSyncCount = 1;
            
            Debug.Log("Islas Apocalipsis - Game Initialized");
        }

        private void Update()
        {
            if (!isPaused)
            {
                gameTime += Time.deltaTime;
            }

            // Quick pause toggle
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            
            // Trigger pause event for UI
            if (UIManager.Instance != null)
            {
                if (isPaused)
                    UIManager.Instance.ShowPauseMenu();
                else
                    UIManager.Instance.HidePauseMenu();
            }
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
