using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEditor.Rendering;

[Serializable]

public struct Level
{
    public int time;
    [Multiline]
    public string commend;
    public Sprite sprite;
}


public class EndControl : MonoBehaviour
{
    [SerializeField] private GameObject reloader;

    public TMP_Text result;
    public TMP_Text p1;
    public TMP_Text p2;
    public TMP_Text tip;
    public Image image;

    private bool hasRead = false;
    private float myTime = 0f;
    private float otherTime = 0f;
    private int resultTime = 0;
    private bool displayTip = false;
    private bool getTime = false;
    private bool showLevel = false;
    private bool levelloaded = false;

    [Header("评价对应等级（单位：秒）")]
    public List<Level> levelList = new List<Level>();
    [Header("进入前5提示")]
    [Multiline] 
    public string EnterTop5Text;
    public Sprite EnterTop5TextSprite;


    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener<float>(EventDic.GameDuration, CompareTime);
        if (!NetMsgMgr.Instance.hasReadRank)
            Client.Instance.Send("GetRank:");
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener<float>(EventDic.GameDuration, CompareTime);
    }
    void Start()
    {
        myTime = Timer.Instance.timeResult;
        Invoke("SendTime", Settings.NetDelayTime); //防止另一方awake还没调用事件还未绑定就已经发送数据
        Check();
    }
    public void CompareTime(float x)
    {
        otherTime = x;
        Check();
    }
    private void SendTime()
    {
        NetMsgMgr.Instance.SendMsg<float>(EventDic.GameDuration, myTime);
    }

    private void Check()
    {
        if (myTime > 0f && otherTime > 0f)
        {
            resultTime = (int)(myTime >= otherTime ? otherTime : myTime);
            Display();
            getTime = true;
        }

    }

    private void Display()
    {
        result.text = SecToMin(resultTime);

        if (GameManager.Instance.MyPlayer == Player.p1)
        {
            p1.text = GameManager.Instance.PlayerName;
            p2.text = GameManager.Instance.OtherPlayerName;
        }
        else if (GameManager.Instance.MyPlayer == Player.p2)
        {
            p1.text = GameManager.Instance.OtherPlayerName;
            p2.text = GameManager.Instance.PlayerName;
        }
    }

    public void DisplayLevel()
    {
        if (displayTip)
        {
            image.sprite = EnterTop5TextSprite;
            image.color = new Color(1, 1, 1, 1);
            tip.text = EnterTop5Text;
            EventMgr.GetInstance().InvokeEvent(EventDic.GameResultEnterTop5);
        }

        else
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.GameResult);
            for (int i = 0; i < levelList.Count; i++)
            {
                if (resultTime <= levelList[i].time)
                {
                    tip.text = levelList[i].commend;
                    if (levelList[i].sprite != null)
                    {
                        image.sprite = levelList[i].sprite;
                        image.color = new Color(1, 1, 1, 1);
                    }
                    break;
                }
            }
        }
    }

    private void ReadRank()
    {
        int t_last = NetMsgMgr.Instance.rank[4].time;
        if (resultTime <= t_last)
        {
            if (GameManager.Instance.MyPlayer == Player.p1)
                Client.Instance.Send($"NewRank:{resultTime}");
            displayTip = true;
        }
        levelloaded = true;
    }

    private string SecToMin(int sec)
    {
        int minute = sec / 60;
        int second = sec % 60;
        if(minute == 0)
            return $"{second:D2}s";
        else
            return $"{minute}min {second:D2}s";
    }

    void Update()
    {
        if (!hasRead && getTime && NetMsgMgr.Instance.hasReadRank)
        {
            ReadRank();
            hasRead = true;
        }
        if (!showLevel && levelloaded)
        {
            DisplayLevel();
            showLevel = true;
        }
    }

    public void Back()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        var g = GameObject.Instantiate(reloader);
        g.GetComponent<Reloader>().SetType(false);
        Client.Instance.ExitRoom();
    }
}
