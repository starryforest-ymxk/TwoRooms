using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class BGMMgr : MonoSingleton<BGMMgr>
{

    public float BGMVolume = 0.75f;
    public float SFXVolume = 0.75f;

    public float bottomVolumeDB = -60f;
    public float topVolumeDB = 20f;

    public AudioMixer audioMixer;

    private void Start()
    {
        BGMVolume = TemporaryData.MusicVolume;
        SFXVolume = TemporaryData.MusicVolume;
        SetVolume(TemporaryData.MusicVolume);
    }

    public void SetVolume(float volume)
    {
        BGMVolume = volume;
        SFXVolume = volume;
        audioMixer.SetFloat("MasterVolume", bottomVolumeDB + volume*(topVolumeDB-bottomVolumeDB));
    }

}
