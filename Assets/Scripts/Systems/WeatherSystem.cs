using UnityEngine;

namespace IslasApocalipsis.Systems
{
    /// <summary>
    /// Weather types in the game
    /// </summary>
    public enum WeatherType
    {
        Clear,
        Rain,
        Storm,
        Fog,
        Snow,
        Sandstorm
    }

    /// <summary>
    /// Dynamic weather system that affects gameplay
    /// </summary>
    public class WeatherSystem : MonoBehaviour
    {
        private static WeatherSystem _instance;
        public static WeatherSystem Instance => _instance;

        [Header("Current Weather")]
        public WeatherType currentWeather = WeatherType.Clear;
        public float weatherIntensity = 0f;

        [Header("Weather Settings")]
        public float weatherChangeInterval = 300f; // 5 minutes
        public bool randomWeather = true;
        public bool affectsGameplay = true;

        [Header("Effects")]
        public ParticleSystem rainEffect;
        public ParticleSystem snowEffect;
        public ParticleSystem fogEffect;
        public Light sunLight;
        public Color clearSkyColor = Color.blue;
        public Color stormySkyColor = Color.gray;

        [Header("Gameplay Impact")]
        public float rainClimbingPenalty = 0.5f; // 50% harder to climb
        public float stormDamagePerSecond = 1f;
        public float fogVisibilityReduction = 0.5f;

        private float weatherTimer = 0f;
        private Material skyboxMaterial;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            skyboxMaterial = RenderSettings.skybox;
            UpdateWeatherEffects();
        }

        private void Update()
        {
            if (randomWeather)
            {
                weatherTimer += Time.deltaTime;
                if (weatherTimer >= weatherChangeInterval)
                {
                    ChangeWeather();
                    weatherTimer = 0f;
                }
            }

            ApplyWeatherEffects();
        }

        private void ChangeWeather()
        {
            // Randomly select new weather
            WeatherType[] weatherTypes = (WeatherType[])System.Enum.GetValues(typeof(WeatherType));
            WeatherType newWeather = weatherTypes[Random.Range(0, weatherTypes.Length)];
            
            SetWeather(newWeather, Random.Range(0.3f, 1f));
        }

        public void SetWeather(WeatherType weather, float intensity = 1f)
        {
            currentWeather = weather;
            weatherIntensity = Mathf.Clamp01(intensity);
            
            Debug.Log($"Weather changed to: {weather} (Intensity: {intensity})");
            
            UpdateWeatherEffects();
        }

        private void UpdateWeatherEffects()
        {
            // Disable all effects first
            if (rainEffect != null) rainEffect.gameObject.SetActive(false);
            if (snowEffect != null) snowEffect.gameObject.SetActive(false);
            if (fogEffect != null) fogEffect.gameObject.SetActive(false);

            // Enable appropriate effect
            switch (currentWeather)
            {
                case WeatherType.Rain:
                    if (rainEffect != null)
                    {
                        rainEffect.gameObject.SetActive(true);
                        var rainEmission = rainEffect.emission;
                        rainEmission.rateOverTime = 100f * weatherIntensity;
                    }
                    UpdateLighting(0.6f);
                    break;

                case WeatherType.Storm:
                    if (rainEffect != null)
                    {
                        rainEffect.gameObject.SetActive(true);
                        var stormEmission = rainEffect.emission;
                        stormEmission.rateOverTime = 200f * weatherIntensity;
                    }
                    UpdateLighting(0.4f);
                    // Lightning could be added here
                    break;

                case WeatherType.Fog:
                    if (fogEffect != null)
                    {
                        fogEffect.gameObject.SetActive(true);
                    }
                    RenderSettings.fog = true;
                    RenderSettings.fogDensity = 0.05f * weatherIntensity;
                    UpdateLighting(0.7f);
                    break;

                case WeatherType.Snow:
                    if (snowEffect != null)
                    {
                        snowEffect.gameObject.SetActive(true);
                        var snowEmission = snowEffect.emission;
                        snowEmission.rateOverTime = 50f * weatherIntensity;
                    }
                    UpdateLighting(0.8f);
                    break;

                case WeatherType.Clear:
                    RenderSettings.fog = false;
                    UpdateLighting(1f);
                    break;
            }
        }

        private void UpdateLighting(float lightIntensity)
        {
            if (sunLight != null)
            {
                sunLight.intensity = lightIntensity;
                
                if (currentWeather == WeatherType.Storm)
                {
                    sunLight.color = stormySkyColor;
                }
                else
                {
                    sunLight.color = Color.Lerp(stormySkyColor, Color.white, lightIntensity);
                }
            }
        }

        private void ApplyWeatherEffects()
        {
            if (!affectsGameplay) return;

            // Find player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;

            var playerStats = player.GetComponent<Player.PlayerStats>();
            var climbingSystem = player.GetComponent<Player.ClimbingSystem>();

            switch (currentWeather)
            {
                case WeatherType.Rain:
                    // Make climbing harder
                    if (climbingSystem != null && climbingSystem.IsClimbing)
                    {
                        float extraStaminaCost = playerStats.climbStaminaCost * rainClimbingPenalty * Time.deltaTime;
                        playerStats.ConsumeStamina(extraStaminaCost);
                    }
                    break;

                case WeatherType.Storm:
                    // Deal damage during storms
                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(stormDamagePerSecond * weatherIntensity * Time.deltaTime);
                    }
                    break;

                case WeatherType.Fog:
                    // Reduce visibility (handled by fog settings)
                    break;

                case WeatherType.Snow:
                    // Could slow movement
                    break;
            }
        }

        public bool IsRaining() => currentWeather == WeatherType.Rain || currentWeather == WeatherType.Storm;
        public bool IsStorming() => currentWeather == WeatherType.Storm;
        public float GetClimbingModifier() => IsRaining() ? (1f + rainClimbingPenalty) : 1f;
    }
}
