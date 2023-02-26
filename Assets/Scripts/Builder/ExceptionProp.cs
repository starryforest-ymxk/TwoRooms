using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExceptionProp : MonoBehaviour
{
    public GameObject Tip_OtherPlayerLeave;
    public GameObject Tip_SeverOff;
    public GameObject Tip_SeverDisconnected;
    public GameObject Tip_SeverMsg;
    public TMP_Text ServerMessage;
    public GameObject reloader;

    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.OtherPlayerLeave, OnOtherPlayerLeave);
        EventMgr.GetInstance().AddEventListener(EventDic.SeverOff, OnSeverOff);
        EventMgr.GetInstance().AddEventListener(EventDic.SeverDisconnected, OnSeverDisconnected);
        EventMgr.GetInstance().AddEventListener<string>(EventDic.ServerMsg, ServerMsg);
        CloseAll();
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OtherPlayerLeave, OnOtherPlayerLeave);
        EventMgr.GetInstance().DeleteEventListener(EventDic.SeverOff, OnSeverOff);
        EventMgr.GetInstance().DeleteEventListener(EventDic.SeverDisconnected, OnSeverDisconnected);
        EventMgr.GetInstance().DeleteEventListener<string>(EventDic.ServerMsg, ServerMsg);
        CloseAll();
    }
    private void ServerMsg(string msg)
    {
        ServerMessage.text = msg;
        Tip_SeverMsg.SetActive(true);
        GameManager.Instance.Exception = true;
    }



    private void OnOtherPlayerLeave()
    {
        CloseAll();
        Tip_OtherPlayerLeave.SetActive(true);
        GameManager.Instance.Exception = true;
    }

    private void OnSeverOff()
    {
        CloseAll();
        Tip_SeverOff.SetActive(true);
        GameManager.Instance.Exception = true;
    }

    private void OnSeverDisconnected()
    {
        CloseAll();
        Tip_SeverDisconnected.SetActive(true);
        GameManager.Instance.Exception = true;
    }

    public void CloseMsg()
    {
        Tip_SeverMsg.SetActive(false);
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        GameManager.Instance.Exception = false;
    }

    public void ExitAndDisconnect()
    {
        CloseAll();
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        GameManager.Instance.Exception = false;
        var g = GameObject.Instantiate(reloader);
        g.GetComponent<Reloader>().SetType(false);

    }

    public void ExitAndConnect()
    {
        CloseAll();
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        GameManager.Instance.Exception = false;
        var g = GameObject.Instantiate(reloader);
        g.GetComponent<Reloader>().SetType(false);

    }

    private void CloseAll()
    {
        Tip_OtherPlayerLeave.SetActive(false);
        Tip_SeverOff.SetActive(false);
        Tip_SeverDisconnected.SetActive(false);
    }



}
