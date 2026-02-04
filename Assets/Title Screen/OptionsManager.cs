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
        Resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        var Options = new System.Collections.Generic.List<string>();
        int CurrentResolutionIndex = 0;

        for (int i = 0; i < Resolutions.Length; i++)
        {
            string Option = Resolutions[i].width + " x " + Resolutions[i].height;
            Options.Add(Option);

            if (Resolutions[i].width == Screen.currentResolution.width && Resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }

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
