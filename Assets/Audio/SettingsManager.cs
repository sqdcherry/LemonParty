using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private RectTransform notificationButton;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private TMP_Text currentLanguage;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    [SerializeField] private AudioMixer mixer;

    public List<string> languages = new List<string>();

    public int isOn;

    private void Awake()
    {
        //SetStartLanguage();
    }

    private void Start()
    {
        //SetStartNotification();
        SetStartSliderValue();
    }

    private void SetStartNotification()
    {
        if (PlayerPrefs.HasKey("Notification"))
        {
            if (PlayerPrefs.GetInt("Notification") == 0)
            {
                isOn = 0;
                notificationButton.anchoredPosition = new Vector2(-34f, notificationButton.anchoredPosition.y);
                notificationText.text = "OFF";
                notificationText.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(100f, notificationText.GetComponent<RectTransform>().anchoredPosition.y);
            }
            else
            {
                isOn = 1;
                notificationButton.anchoredPosition = new Vector2(31f, notificationButton.anchoredPosition.y);
                notificationText.text = "ON";
                notificationText.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(39f, notificationText.GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Notification", 1);
            isOn = 1;
            notificationButton.anchoredPosition = new Vector2(31f, notificationButton.anchoredPosition.y);
            notificationText.text = "ON";
            notificationText.GetComponent<RectTransform>().anchoredPosition = new Vector2(31f, notificationText.transform.position.y);
        }
    }

    private void SetStartLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            if (PlayerPrefs.GetInt("Language") == 0)
            {
                currentLanguage.text = "English";
            }
            else if (PlayerPrefs.GetInt("Language") == 1)
            {
                currentLanguage.text = "Spanish";
            }
            else if (PlayerPrefs.GetInt("Language") == 2)
            {
                currentLanguage.text = "Germany";
            }
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLanguage", 0);
            currentLanguage.text = "English";
        }
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

    //public void SetLanguage(bool value)
    //{
    //    int currentStage;
    //    if (PlayerPrefs.HasKey("Language"))
    //        currentStage = PlayerPrefs.GetInt("Language");
    //    else
    //        currentStage = 0;

    //    if (value)
    //    {
    //        int nexttStage = currentStage + 1;

    //        if (nexttStage < 3)
    //        {
    //            PlayerPrefs.SetInt("Language", nexttStage);
    //            currentLanguage.text = languages[nexttStage];
    //            StartCoroutine(LocaleSelector.instance.SetLocale(nexttStage));
    //        }
    //        else
    //        {
    //            PlayerPrefs.SetInt("Language", 0);
    //            currentLanguage.text = languages[0];
    //            StartCoroutine(LocaleSelector.instance.SetLocale(0));
    //        }
    //    }
    //    else
    //    {
    //        int nexttStage = currentStage - 1;

    //        if (nexttStage > -1)
    //        {
    //            PlayerPrefs.SetInt("Language", nexttStage);
    //            currentLanguage.text = languages[nexttStage];
    //            StartCoroutine(LocaleSelector.instance.SetLocale(nexttStage));
    //        }
    //        else
    //        {
    //            PlayerPrefs.SetInt("Language", 2);
    //            currentLanguage.text = languages[2];
    //            StartCoroutine(LocaleSelector.instance.SetLocale(2));
    //        }
    //    }
    //}

    //public void SetNotificalActive()
    //{
    //    if (isOn == 1)
    //    {
    //        isOn = 0;
    //        notificationButton.anchoredPosition = new Vector2(-34f, notificationButton.anchoredPosition.y);
    //        notificationText.text = "OFF";
    //        notificationText.GetComponent<RectTransform>().anchoredPosition =
    //            new Vector2(100f, notificationText.GetComponent<RectTransform>().anchoredPosition.y);
    //        PlayerPrefs.SetInt("Notification", 0);
    //    }
    //    else
    //    {
    //        isOn = 1;
    //        notificationButton.anchoredPosition = new Vector2(31f, notificationButton.anchoredPosition.y);
    //        notificationText.text = "ON";
    //        notificationText.GetComponent<RectTransform>().anchoredPosition =
    //            new Vector2(39f, notificationText.GetComponent<RectTransform>().anchoredPosition.y);
    //        PlayerPrefs.SetInt("Notification", 1);
    //    }
    //}
    
    public void OnRate()
    {
        Application.OpenURL("google.com");
    }

    public void OnBugReport()
    {
        Application.OpenURL("google.com");
    }

    public void OnLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}