using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        // Kayıtlı ses ayarlarını yükle
        musicToggle.isOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        sfxToggle.isOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // Eventleri bağla
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        sfxToggle.onValueChanged.AddListener(ToggleSFX);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        ApplySettings();
    }

    private void ApplySettings()
    {
        AudioManager.Instance.SetMusicVolume(musicToggle.isOn ? musicSlider.value : 0);
        AudioManager.Instance.SetSFXVolume(sfxToggle.isOn ? sfxSlider.value : 0);
    }

    public void ToggleMusic(bool isOn)
    {
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        AudioManager.Instance.SetMusicVolume(isOn ? musicSlider.value : 0);
    }

    public void ToggleSFX(bool isOn)
    {
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        AudioManager.Instance.SetSFXVolume(isOn ? sfxSlider.value : 0);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        if (musicToggle.isOn)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        if (sfxToggle.isOn)
        {
            AudioManager.Instance.SetSFXVolume(volume);
        }
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
