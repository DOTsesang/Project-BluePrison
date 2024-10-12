using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RobustGraphicsSettingsManager : MonoBehaviour
{
    public static RobustGraphicsSettingsManager Instance { get; private set; }

    private Dictionary<string, bool> settingsState = new Dictionary<string, bool>();
    private float rayTracingQuality;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSettings();
        ApplySettingsToCurrentScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySettingsToCurrentScene();
        SetupUIListeners();
    }

    private void SetupUIListeners()
    {
        SetToggleListener("SSAO", "ssaoToggle");
        SetToggleListener("SSR", "ssrToggle");
        SetToggleListener("SSGI", "ssgiToggle");
        SetToggleListener("RayTracing", "rayTracingToggle");
        SetSliderListener("RayTracingQuality", "rayTracingQualitySlider");
    }

    private void SetToggleListener(string settingName, string toggleName)
    {
        Toggle toggle = GameObject.Find(toggleName)?.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.isOn = settingsState[settingName];
            toggle.onValueChanged.AddListener(value => { UpdateSetting(settingName, value); });
        }
    }

    private void SetSliderListener(string settingName, string sliderName)
    {
        Slider slider = GameObject.Find(sliderName)?.GetComponent<Slider>();
        if (slider != null)
        {
            slider.value = rayTracingQuality;
            slider.onValueChanged.AddListener(value => { UpdateRayTracingQuality(value); });
        }
    }

    private void UpdateSetting(string settingName, bool value)
    {
        settingsState[settingName] = value;
        SaveSettings();
        ApplySettingsToCurrentScene();
    }

    private void UpdateRayTracingQuality(float value)
    {
        rayTracingQuality = value;
        SaveSettings();
        ApplySettingsToCurrentScene();
    }

    private void ApplySettingsToCurrentScene()
    {
        Volume[] volumes = FindObjectsOfType<Volume>();
        foreach (Volume volume in volumes)
        {
            ApplySettingsToVolume(volume);
        }
    }

    private void ApplySettingsToVolume(Volume volume)
    {
        if (volume == null || volume.profile == null) return;

        ApplySettingToVolume<ScreenSpaceAmbientOcclusion>(volume, "SSAO");
        ApplySettingToVolume<ScreenSpaceReflection>(volume, "SSR");
        ApplySettingToVolume<GlobalIllumination>(volume, "SSGI");
        ApplySettingToVolume<PathTracing>(volume, "RayTracing");
    }

    private void ApplySettingToVolume<T>(Volume volume, string settingName) where T : VolumeComponent
    {
        if (volume.profile.TryGet(out T settings) && settingsState.TryGetValue(settingName, out bool isEnabled))
        {
            settings.active = isEnabled;
            if (settings is PathTracing pathTracing)
            {
                pathTracing.maximumSamples.value = Mathf.RoundToInt(rayTracingQuality * 1000);
            }
        }
    }

    private void SaveSettings()
    {
        foreach (var setting in settingsState)
        {
            PlayerPrefs.SetInt(setting.Key, setting.Value ? 1 : 0);
        }
        PlayerPrefs.SetFloat("RayTracingQuality", rayTracingQuality);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        settingsState["SSAO"] = PlayerPrefs.GetInt("SSAO", 1) == 1;
        settingsState["SSR"] = PlayerPrefs.GetInt("SSR", 1) == 1;
        settingsState["SSGI"] = PlayerPrefs.GetInt("SSGI", 1) == 1;
        settingsState["RayTracing"] = PlayerPrefs.GetInt("RayTracing", 0) == 1;
        rayTracingQuality = PlayerPrefs.GetFloat("RayTracingQuality", 0.5f);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}