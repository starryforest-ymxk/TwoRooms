using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4轮盘Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip rotate;
    public AudioClip open;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PARotateWheel, Rotate);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Wheel_IsDone, Open);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PARotateWheel, Rotate);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Wheel_IsDone, Open);
    }

    public void Rotate()
    {
        audioSource.clip = rotate;
        audioSource.Play();
    }

    public void Open()
    {
        audioSource.clip = open;
        audioSource.Play();
    }
}
