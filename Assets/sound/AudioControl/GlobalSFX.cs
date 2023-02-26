using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSFX : MonoBehaviour
{
    public AudioSource SFXaudio;
    public AudioClip pickItem;
    public AudioClip teleportClick;
    public AudioClip showClue;
    public AudioClip closeClue;

    public AudioClip exception;
    public AudioClip msg;

    public AudioClip clickUI;
    public AudioClip UiPop;

    public AudioClip gameEnd;
    public AudioClip gameEndEnterTop5;


    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener<ItemName>(EventDic.OnPick, OnPick);
        EventMgr.GetInstance().AddEventListener(EventDic.OnClickTeleport, OnClickTeleport);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAShowColorClue, OnShowClue);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBShowMap, OnShowClue);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBShowSignClue, OnShowClue);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_CloseClue, OnCloseClue);

        EventMgr.GetInstance().AddEventListener(EventDic.OtherPlayerLeave, Exception);
        EventMgr.GetInstance().AddEventListener(EventDic.SeverOff, Exception);
        EventMgr.GetInstance().AddEventListener(EventDic.SeverDisconnected, Exception);
        EventMgr.GetInstance().AddEventListener<string>(EventDic.ServerMsg, ServerMsg);

        EventMgr.GetInstance().AddEventListener(EventDic.OnClickUI, OnClickUI);
        EventMgr.GetInstance().AddEventListener(EventDic.UIpop, OnUIpop);

        EventMgr.GetInstance().AddEventListener(EventDic.GameResultEnterTop5, OnGameEndEnterTop5);
        EventMgr.GetInstance().AddEventListener(EventDic.GameResult, OnGameEnd);

    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener<ItemName>(EventDic.OnPick, OnPick);
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnClickTeleport, OnClickTeleport);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAShowColorClue, OnShowClue);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBShowMap, OnShowClue);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBShowSignClue, OnShowClue);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_CloseClue, OnCloseClue);

        EventMgr.GetInstance().DeleteEventListener(EventDic.OtherPlayerLeave, Exception);
        EventMgr.GetInstance().DeleteEventListener(EventDic.SeverOff, Exception);
        EventMgr.GetInstance().DeleteEventListener(EventDic.SeverDisconnected, Exception);
        EventMgr.GetInstance().DeleteEventListener<string>(EventDic.ServerMsg, ServerMsg);

        EventMgr.GetInstance().DeleteEventListener(EventDic.OnClickUI, OnClickUI);
        EventMgr.GetInstance().DeleteEventListener(EventDic.UIpop, OnUIpop);

        EventMgr.GetInstance().DeleteEventListener(EventDic.GameResultEnterTop5, OnGameEndEnterTop5);
        EventMgr.GetInstance().DeleteEventListener(EventDic.GameResult, OnGameEnd);


    }

    private void OnPick(ItemName item)
    {
        SFXaudio.clip = pickItem;
        SFXaudio.Play();
    }
    public void OnClickTeleport()
    {
        SFXaudio.clip = teleportClick;
        SFXaudio.Play();
    }

    public void OnShowClue()
    {
        SFXaudio.clip = showClue;
        SFXaudio.Play();
    }

    public void OnCloseClue()
    {
        SFXaudio.clip = closeClue;
        SFXaudio.Play();
    }
    public void ServerMsg(string m)
    {
        SFXaudio.clip = msg;
        SFXaudio.Play();
    }
    public void Exception()
    {
        SFXaudio.clip = exception;
        SFXaudio.Play();
    }

    public void OnClickUI()
    {
        SFXaudio.clip = clickUI;
        SFXaudio.Play();
    }

    public void OnUIpop()
    {
        SFXaudio.clip = UiPop;
        SFXaudio.Play();
    }

    public void OnGameEndEnterTop5()
    {
        SFXaudio.clip = gameEndEnterTop5;
        SFXaudio.Play();
    }

    public void OnGameEnd()
    {
        SFXaudio.clip = gameEnd;
        SFXaudio.Play();
    }

}
