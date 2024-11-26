using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        SetStartSliderValue();
    }


    private void SetStartSliderValue()
    {
        if (musicSlider && soundsSlider)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            else
                musicSlider.value = musicSlider.maxValue;
            if (PlayerPrefs.HasKey("SFXVolume"))
                soundsSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            else
                soundsSlider.value = musicSlider.maxValue;
        }
    }

    public void UpdateMusicValueOnChange(float sliderValue)
    {
        float volumeValue = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("Music", volumeValue);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void UpdateSoundValueOnChange(float sliderValue)
    {
        float volumeValue = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("SFX", volumeValue);
        PlayerPrefs.SetFloat("SFXVolume", soundsSlider.value);
    }
    
    public void OnRate()
    {
        Application.OpenURL("google.com");
    }

    public void OnBugReport()
    {
        Application.OpenURL("google.com");
    }
}