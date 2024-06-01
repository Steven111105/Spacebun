using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    private Resolution[] resolutions;


    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        LoadSettings();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveSettings();
    }
    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
            SaveSettings();
        }
    }
    public void SetVolume(float volume)
    {
        volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        AudioListener.volume = volume;
        SaveSettings();
    }

    
    public void LoadSettings()
    {
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;

        int resolutionIndex = PlayerPrefs.GetInt("Resolution", 0);
        resolutionDropdown.value = resolutionIndex;
        SetResolution(resolutionIndex);

        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SetVolume(volume);
    }
    public void SaveSettings()
    {
        int isFullscreen = fullscreenToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen);

        int resolutionIndex = resolutionDropdown.value;
        PlayerPrefs.SetInt("Resolution", resolutionIndex);

        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);

        PlayerPrefs.Save();
    }
}