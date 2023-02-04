using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, optionsMenu;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    [Header("Default volumes")]
    [SerializeField] private float defaultMasterVolume = 0.7f;
    [SerializeField] private float defaultMusicVolume = 0.7f;
    [SerializeField] private float defaultEffectsVolume = 0.7f;
    
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        var resolutionOptions = new List<string>();

        var currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add($"{resolutions[i].width} x {resolutions[i].height}");

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(new List<string>(Array.ConvertAll(resolutions, resolution => $"{resolution.width} x {resolution.height}")));
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        fullscreenToggle.isOn = Screen.fullScreen;
        
        SetMasterVolume(defaultMasterVolume);
        SetMusicVolume(defaultMusicVolume);
        SetEffectsVolume(defaultEffectsVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", LinearToDecibel(volume));
    }
    
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", LinearToDecibel(volume));
    }
    
    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectsVolume", LinearToDecibel(volume));
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void OnBack()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    
    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;
        return dB;
    }
}