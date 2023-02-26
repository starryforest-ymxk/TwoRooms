using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3_B4_数猜 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip set;
    public AudioClip pick;
    public AudioClip pop;
    public AudioClip buttonPop;
    public AudioClip stoneMove;



    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PlayerClickButton, Click);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ButtonPop, ButtonPop);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ButtonGameCompleted, StoneMove);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutSign, Set);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPickSign, Pick);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBSignPop, Pop);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_NumGameCompletedStoneMove, StoneMove);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPickSign, Pick);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutSign, Set);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PlayerClickButton, Click);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ButtonPop, ButtonPop);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBSignPop, Pop);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ButtonGameCompleted, StoneMove);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_NumGameCompletedStoneMove, StoneMove);
    }

    public void Set()
    {
        audioSource.clip = set;
        audioSource.Play();
    }
    private void Click()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    private void Pick()
    {
        audioSource.clip = pick;
        audioSource.Play();
    }
    private void ButtonPop()
    {
        audioSource.clip = buttonPop;
        audioSource.Play();
    }
    private void Pop()
    {
        audioSource.clip = pop;
        audioSource.Play();
    }
    private void StoneMove()
    {
        audioSource.clip = stoneMove;
        audioSource.Play();
    }
}
