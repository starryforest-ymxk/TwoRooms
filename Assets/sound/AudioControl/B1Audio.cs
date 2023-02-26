using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class B1Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pushSun;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPushSunLocal, PushSun);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPushSunLocal, PushSun);
    }
    public void PushSun()
    {
        audioSource.clip = pushSun;
        audioSource.Play();
    }
}
