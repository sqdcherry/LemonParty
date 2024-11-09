using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicAudioSource;
    public AudioSource soundsAudioSource;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        if (PlayerPrefs.HasKey("SFXVolume"))
            audioMixer.SetFloat("Sounds", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
    }

    public void PlaySoundEffect(AudioClip audioCLip)
    {
        soundsAudioSource.clip = audioCLip;
        soundsAudioSource.Play();
    }
}