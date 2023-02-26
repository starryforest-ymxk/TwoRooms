using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4文排Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip put;
    public AudioClip gateOpen;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PlayerPutSign, Put);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_SignGame, GateOpen);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PlayerPutSign, Put);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_SignGame, GateOpen);
    }

    public void Put()
    {
        audioSource.clip = put;
        audioSource.Play();
    }

    public void GateOpen()
    {
        audioSource.clip = gateOpen;
        audioSource.Play();
    }
}
