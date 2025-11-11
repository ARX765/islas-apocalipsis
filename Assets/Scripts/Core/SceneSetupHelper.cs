using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IslasApocalipsis.Core
{
    /// <summary>
    /// Helper script to quickly set up a test scene with placeholder objects
    /// This creates a basic playable scene with player, enemy, and environment
    /// </summary>
    public class SceneSetupHelper : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Islas Apocalipsis/Setup/Create Basic Test Scene")]
        public static void CreateBasicTestScene()
        {
            // Clear existing objects (optional, commented for safety)
            // ClearScene();

            // Create environment
            CreateGround();
            CreateLighting();
            
            // Create player
            CreatePlayer();
            
            // Create camera system
            CreateCameraSystem();
            
            // Create managers
            CreateGameManagers();
            
            // Create test enemy
            CreateTestEnemy();
            
            Debug.Log("Basic test scene created! Remember to bake NavMesh: Window → AI → Navigation → Bake");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Player")]
        public static void CreatePlayer()
        {
            // Check if player already exists
            GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
            if (existingPlayer != null)
            {
                Debug.LogWarning("Player already exists in scene!");
                Selection.activeGameObject = existingPlayer;
                return;
            }

            // Create player root
            GameObject player = new GameObject("Player");
            player.tag = "Player";
            player.layer = LayerMask.NameToLayer("Default");
            player.transform.position = new Vector3(0, 2, 0);

            // Add Character Controller
            CharacterController controller = player.AddComponent<CharacterController>();
            controller.radius = 0.5f;
            controller.height = 2f;
            controller.center = new Vector3(0, 1, 0);

            // Add Player Scripts
            player.AddComponent<Player.PlayerController>();
            player.AddComponent<Player.PlayerStats>();
            player.AddComponent<Player.ClimbingSystem>();
            player.AddComponent<Combat.CombatSystem>();
            player.AddComponent<Magic.MagicSystem>();
            player.AddComponent<Combat.WeaponManager>();

            // Create visual representation
            GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "PlayerVisual";
            visual.transform.SetParent(player.transform);
            visual.transform.localPosition = new Vector3(0, 1, 0);
            visual.transform.localScale = Vector3.one;
            DestroyImmediate(visual.GetComponent<Collider>()); // Remove collider, using CharacterController

            // Create material
            Material playerMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            playerMat.color = new Color(0f, 0.5f, 1f); // Blue
            visual.GetComponent<Renderer>().material = playerMat;

            // Create ground check
            GameObject groundCheck = new GameObject("GroundCheck");
            groundCheck.transform.SetParent(player.transform);
            groundCheck.transform.localPosition = new Vector3(0, 0, 0);

            // Create attack point
            GameObject attackPoint = new GameObject("AttackPoint");
            attackPoint.transform.SetParent(player.transform);
            attackPoint.transform.localPosition = new Vector3(0, 1, 0.5f);

            // Create weapon slots
            GameObject rightHand = new GameObject("RightHandSlot");
            rightHand.transform.SetParent(player.transform);
            rightHand.transform.localPosition = new Vector3(0.3f, 1.2f, 0.3f);

            GameObject leftHand = new GameObject("LeftHandSlot");
            leftHand.transform.SetParent(player.transform);
            leftHand.transform.localPosition = new Vector3(-0.3f, 1.2f, 0.3f);

            // Create cast point for magic
            GameObject castPoint = new GameObject("CastPoint");
            castPoint.transform.SetParent(player.transform);
            castPoint.transform.localPosition = new Vector3(0, 1.5f, 0.5f);

            Selection.activeGameObject = player;
            Debug.Log("Player created at (0, 2, 0). Configure references in Inspector.");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Test Enemy")]
        public static void CreateTestEnemy()
        {
            GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemy.name = "CorruptedScout";
            enemy.tag = "Enemy";
            enemy.layer = LayerMask.NameToLayer("Default");
            enemy.transform.position = new Vector3(5, 1, 5);

            // Create material
            Material enemyMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            enemyMat.color = Color.red;
            enemy.GetComponent<Renderer>().material = enemyMat;

            // Add NavMesh Agent
            NavMeshAgent agent = enemy.AddComponent<NavMeshAgent>();
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
            agent.acceleration = 8f;

            // Add Enemy Scripts
            Enemies.EnemyAI ai = enemy.AddComponent<Enemies.EnemyAI>();
            Enemies.EnemyHealth health = enemy.AddComponent<Enemies.EnemyHealth>();
            
            // Configure health
            health.enemyType = Enemies.EnemyType.CorruptedScout;
            health.maxHealth = 50f;

            Selection.activeGameObject = enemy;
            Debug.Log("Test enemy created. Remember to bake NavMesh!");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Ground")]
        public static void CreateGround()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Ground";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(10, 1, 10);

            // Create material
            Material groundMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            groundMat.color = new Color(0.3f, 0.6f, 0.3f); // Green
            ground.GetComponent<Renderer>().material = groundMat;

            // Make it static for NavMesh
            ground.isStatic = true;

            Debug.Log("Ground created. Mark as Navigation Static for NavMesh.");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Lighting")]
        public static void CreateLighting()
        {
            // Check if directional light exists
            Light existingLight = FindObjectOfType<Light>();
            if (existingLight != null && existingLight.type == LightType.Directional)
            {
                Debug.Log("Directional light already exists.");
                return;
            }

            GameObject lightGO = new GameObject("Directional Light");
            Light light = lightGO.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1f;
            light.color = Color.white;
            lightGO.transform.rotation = Quaternion.Euler(50, -30, 0);

            Debug.Log("Directional light created.");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Camera System")]
        public static void CreateCameraSystem()
        {
            // Find existing camera
            Camera mainCam = Camera.main;
            
            GameObject cameraSystem = new GameObject("CameraSystem");
            cameraSystem.AddComponent<CameraController>();

            if (mainCam != null)
            {
                mainCam.transform.SetParent(cameraSystem.transform);
            }
            else
            {
                GameObject camGO = new GameObject("Main Camera");
                camGO.tag = "MainCamera";
                Camera cam = camGO.AddComponent<Camera>();
                camGO.AddComponent<AudioListener>();
                camGO.transform.SetParent(cameraSystem.transform);
                camGO.transform.localPosition = new Vector3(0, 5, -10);
            }

            Debug.Log("Camera system created. Set target to Player in Inspector.");
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Game Managers")]
        public static void CreateGameManagers()
        {
            // Game Manager
            if (FindObjectOfType<GameManager>() == null)
            {
                GameObject gmGO = new GameObject("GameManager");
                gmGO.AddComponent<GameManager>();
                Debug.Log("GameManager created.");
            }

            // UI Manager
            if (FindObjectOfType<UI.UIManager>() == null)
            {
                GameObject uiGO = new GameObject("UIManager");
                uiGO.AddComponent<UI.UIManager>();
                
                // Create Canvas
                GameObject canvas = new GameObject("Canvas");
                Canvas canvasComp = canvas.AddComponent<Canvas>();
                canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvas.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                canvas.transform.SetParent(uiGO.transform);

                // Create EventSystem if it doesn't exist
                if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
                {
                    GameObject eventSystem = new GameObject("EventSystem");
                    eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                    eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                }

                Debug.Log("UIManager and Canvas created.");
            }

            // Save System
            if (FindObjectOfType<Systems.SaveSystem>() == null)
            {
                GameObject saveGO = new GameObject("SaveSystem");
                saveGO.AddComponent<Systems.SaveSystem>();
                Debug.Log("SaveSystem created.");
            }

            // Weather System
            if (FindObjectOfType<Systems.WeatherSystem>() == null)
            {
                GameObject weatherGO = new GameObject("WeatherSystem");
                weatherGO.AddComponent<Systems.WeatherSystem>();
                Debug.Log("WeatherSystem created.");
            }
        }

        [MenuItem("Islas Apocalipsis/Setup/Create Simple Weapon")]
        public static void CreateSimpleWeapon()
        {
            GameObject weapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
            weapon.name = "BasicSword";
            weapon.transform.localScale = new Vector3(0.1f, 1f, 0.1f);

            Material weaponMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            weaponMat.color = new Color(0.7f, 0.7f, 0.7f); // Gray
            weapon.GetComponent<Renderer>().material = weaponMat;

            Selection.activeGameObject = weapon;
            Debug.Log("Simple weapon created. Parent to RightHandSlot in Player.");
        }

        [MenuItem("Islas Apocalipsis/Help/Open Setup Guide")]
        public static void OpenSetupGuide()
        {
            string guidePath = "Assets/../PLACEHOLDER_ASSETS_GUIDE.md";
            if (System.IO.File.Exists(guidePath))
            {
                Application.OpenURL(guidePath);
            }
            else
            {
                Debug.LogWarning("PLACEHOLDER_ASSETS_GUIDE.md not found in project root!");
            }
        }

        private static void ClearScene()
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.transform.parent == null) // Only root objects
                {
                    DestroyImmediate(obj);
                }
            }
        }
#endif
    }
}
