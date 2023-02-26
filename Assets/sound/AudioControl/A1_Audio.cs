using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1_Audio : MonoBehaviour
{
    [Header("»ðÑæÉùÐ§")]
    public AudioSource fireAudio;
    public float flameVolume = 0.3f;
    public float fireLightingVolume = 1f;
    public AudioClip fireLighting;
    public AudioClip flame;
    public float lightingtime;
    private bool Lighted = false;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPushSun, OnLighting);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPushSun, OnLighting);
    }
    private void Start()
    {
        if (GameManager.Instance.Triggers.Game_PBPushSun)
        {
            fireAudio.volume = flameVolume;
            Lighted = true;
            fireAudio.clip = flame;
            fireAudio.Play();
        }
    }
    private void OnLighting()
    {
        if (Lighted)
            return;
        Lighted = true;
        fireAudio.volume = fireLightingVolume;
        fireAudio.clip = fireLighting;
        fireAudio.Play();
        Invoke("Flame", lightingtime);
    }
    private void Flame()
    {
        fireAudio.volume = flameVolume;
        fireAudio.clip = flame;
        fireAudio.Play();
    }
}
