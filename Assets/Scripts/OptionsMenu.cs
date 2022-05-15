using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

  



    public AudioMixer audioMixer;

    public Slider _sliderMaster;
    public Slider _sliderMusic;
    public Slider _sliderEffects;

    private void Start()
    {
        _sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        _sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        _sliderEffects.value = PlayerPrefs.GetFloat("EffectVolume", 1);
        SetVolume(PlayerPrefs.GetFloat("MasterVolume", 1));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1));
        SetSpecialVolume(PlayerPrefs.GetFloat("EffectVolume", 1));
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("music_volume", Mathf.Log10(volume)*20);
    }

    public void SetSpecialVolume(float volume)
    {
        PlayerPrefs.SetFloat("EffectVolume", volume);
        audioMixer.SetFloat("special_volume", Mathf.Log10(volume)*20);
    }
}
