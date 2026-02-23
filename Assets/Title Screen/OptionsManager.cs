using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider VolumeSlider;
    public Toggle FullscreenToggle;
    public TMP_Dropdown ResolutionDropdown;

    Resolution[] Resolutions;

    private void Start()
    {
        {
            LoadVolume();
            SetupResolutions();
            LoadFullscreen();
        }
    }

    // -------------- VOLUME --------------

    public void SetVolume (float Volume)
    {
        AudioListener.volume = Volume;
        PlayerPrefs.SetFloat("MasterVolume", Volume);
    }

    void LoadVolume()
    {
        float Volume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        VolumeSlider.value = Volume;
        AudioListener.volume = Volume;
    }

    // ------------------- FULLSCREEN -------------------

    public void SetFullscreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
        PlayerPrefs.SetInt("Fullscreen", IsFullScreen ? 1 : 0);
    }

    void LoadFullscreen()
    {
        bool IsFullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        FullscreenToggle.isOn = IsFullScreen;
        Screen.fullScreen = IsFullScreen;
    }

    // --------------------- RESOLUTION ---------------------

    void SetupResolutions()
    {
        Resolution[] AllResolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        var Options = new System.Collections.Generic.List<string>();
        var UniqueResolutions = new System.Collections.Generic.List<Resolution>();

        int CurrentResolutionIndex = 0;

        for (int i = 0; i < AllResolutions.Length; i++)
        {
            Resolution res = AllResolutions[i];

            // Check if we already added this width/height combo
            bool Exists = false;
            for (int j = 0; j < UniqueResolutions.Count; j++)
            {
                if (UniqueResolutions[j].width == res.width &&
                    UniqueResolutions[j].height == res.height)
                {
                    Exists = true;
                    break;
                }
            }

            if (Exists)
                continue;

            UniqueResolutions.Add(res);
            Options.Add(res.width + " x " + res.height);

            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = UniqueResolutions.Count - 1;
            }
        }

        Resolutions = UniqueResolutions.ToArray();

        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution Res = Resolutions[ResolutionIndex];
        Screen.SetResolution(Res.width, Res.height, Screen.fullScreen);
    }
}
