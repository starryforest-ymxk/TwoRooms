using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1_祭坛Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip PutClip;
    public AudioClip PickClip;
    public AudioClip OpenGate;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutIdol, OnPut);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPickIdol, OnPick);
        EventMgr.GetInstance().AddEventListener(EventDic.GamePaPutAllIdol, OnOpenGate);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutIdol, OnPut);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPickIdol, OnPick);
        EventMgr.GetInstance().DeleteEventListener(EventDic.GamePaPutAllIdol, OnOpenGate);
    }


    public void OnPut()
    {
        audioSource.clip = PutClip;
        audioSource.Play();
    }

    public void OnPick()
    {
        audioSource.clip = PickClip;
        audioSource.Play();
    }

    public void OnOpenGate()
    {
        audioSource.clip = OpenGate;
        audioSource.Play();
    }
}
