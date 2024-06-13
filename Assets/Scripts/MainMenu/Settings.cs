using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    private Resolution[] resolutions;
    private List<Resolution> uniqueResolutions;

    void Start()
    {
        // Initialize resolution dropdown
        resolutions = Screen.resolutions;
        uniqueResolutions = new List<Resolution>();
        HashSet<string> uniqueResolutionStrings = new HashSet<string>();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + " x " + resolutions[i].height;
            if (!uniqueResolutionStrings.Contains(resolutionString))
            {
                uniqueResolutionStrings.Add(resolutionString);
                uniqueResolutions.Add(resolutions[i]);
                options.Add(resolutionString);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        LoadSettings();

        // Add listeners
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        // Save resolution setting
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // Save fullscreen setting
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        // Save volume setting
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        // Load resolution setting
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(resolutionIndex);
        }
        else
        {
            resolutionDropdown.value = resolutions.Length - 1;
            resolutionDropdown.RefreshShownValue();
            SetResolution(resolutions.Length - 1);
        }

        // Load fullscreen setting
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }
        else
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }

        // Load volume setting
        if (PlayerPrefs.HasKey("Volume"))
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = volume;
            AudioListener.volume = volume;
        }
        else
        {
            volumeSlider.value = AudioListener.volume;
        }
    }
}
