using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1棺木Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickNum;
    public AudioClip open;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Coffin_ClickNum, ClickNum);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Coffin_IsDone, Open);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Coffin_ClickNum, ClickNum);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Coffin_IsDone, Open);
    }
    public void ClickNum()
    {
        audioSource.clip = clickNum;
        audioSource.Play();
    }
    public void Open()
    {
        audioSource.clip = open;
        audioSource.Play();
    }
}
