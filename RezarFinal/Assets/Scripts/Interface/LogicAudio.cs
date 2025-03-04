using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LogicAudio : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const float DefaultVolume = 0f;

    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultVolume);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, DefaultVolume);

        musicMixer.SetFloat("Volume", musicVolume);
        sfxMixer.SetFloat("Volume", sfxVolume);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    private void OnSFXVolumeChanged(float volume)
    {
        sfxMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }
    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
