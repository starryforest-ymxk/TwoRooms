using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_B2_天平 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip put;
    public AudioClip pick;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalancePut, Put);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalancePick, Pick);
        
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalancePut, Put);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalancePick, Pick);

    }

    private void Put()
    {
        audioSource.clip = put;
        audioSource.Play();
    }
    private void Pick()
    {
        audioSource.clip = pick;
        audioSource.Play();
    }
}
