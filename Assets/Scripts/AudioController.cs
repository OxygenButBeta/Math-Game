using Assets.Scripts.SharedLibs.Registry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource SfxSource;
    [SerializeField] AudioSource MusicSource;
    [Header("Clips")]
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip True;
    [SerializeField] AudioClip False;
    [SerializeField] AudioClip Load;
    [Header("Sliders")]
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SfxSlider;

    public RegFloat MusicVolume = new RegFloat("MusicVolume", 0.5f);
    public RegFloat SfxVolume = new RegFloat("SfxVolume", 1);
    public static AudioController Instance { get; private set; }
    private void Start()
    {
        Instance = this;
        MusicSlider.value = MusicVolume;
        SfxSlider.value = SfxVolume;
        SyncSliders();

    }
    public void Sync(Single s)
    {
        SfxSource.volume = SfxSlider.value;
        SfxVolume.Set(SfxSlider.value);
        MusicSource.volume = MusicSlider.value;
        MusicVolume.Set(MusicSlider.value);
    }
    public void SyncSliders()
    {
        SfxSlider.value = SfxVolume;
        MusicSlider.value = MusicVolume;
    }
    public void PlayAudio(Audio audio)
    {
        switch (audio)
        {
            case Audio.Click:
                SfxSource.clip = click;
                break;
            case Audio.False:
                SfxSource.clip = False;
                break;
            case Audio.True:
                SfxSource.clip = True;
                break;
            case Audio.Load:
                SfxSource.clip = Load;
                break;
        }
        SfxSource.Play();
    }
    public enum Audio
    {
        Click,
        False,
        True,
        Load
    }
}
