using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        SetVolume(SaveOptions.MasterVolume);
        SetMusicVolume(SaveOptions.MusicVolume);
        SetSpecialVolume(SaveOptions.EffectVolume);
    }

    public void SetVolume(float volume)
    {
        SaveOptions.MasterVolume = volume;
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume(float volume)
    {
        SaveOptions.MusicVolume = volume;
        audioMixer.SetFloat("music_volume", Mathf.Log10(volume)*20);
    }

    public void SetSpecialVolume(float volume)
    {
        SaveOptions.EffectVolume = volume;
        audioMixer.SetFloat("special_volume", Mathf.Log10(volume)*20);
    }
}
