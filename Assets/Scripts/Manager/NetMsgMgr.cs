using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 用于发送网络信息
/// </summary>

public class NetMsgMgr : MonoSingleton<NetMsgMgr>
{
    public bool readIt;
    public Rank[] rank = new Rank[5];
    public bool hasReadRank = false;

    
    private void Update()
    {
        if (readIt)
        {
            string s = Client.Instance.Messages.Dequeue();
            ExcuteMeg(s);
            Debug.Log($"Server sent: {s}");
            if(Client.Instance.Messages.Count == 0)
                readIt = false;
        }
    }
    #region 发送
    /// <summary>
    /// 只适用无参信息
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="rawMsg"></param>
    public void SendMsg(MsgID msgID, string rawMsg)
    {
        string msg = msgID.ToString() + ':' + rawMsg + '#';
        GameManager.Instance.Client.Send(msg);
    }
    /// <summary>
    /// 适用含一个参数的信息
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="eventName">事件名</param>
    /// <param name="param">参数</param>
    public void SendMsg<T>(string eventName, T param)
    {
        MsgID msgID;
        if (param is int)
        {
            msgID = MsgID.Event_1int;
        }
        else if (param is string)
        {
            msgID = MsgID.Event_1string;
        }
        else if (param is float)
        {
            msgID = MsgID.Event_1float;
        }
        else
        {
            Debug.LogError("错误的信息格式");
            return;
        }
        string msg = msgID.ToString() + ':' + eventName + ':' + param.ToString() + '#';
        GameManager.Instance.Client.Send(msg);
    }
    #endregion
    #region 获取
    public void ExcuteMeg(string msginfo)
    {
        string[] Msg = msginfo.Split('#');
        foreach(string msg in Msg)
        {
            string[] tmp = msg.Split(':');
            try
            {
                var t = Tools.StringToEnum<MsgID>(tmp[0]);
                switch (t)
                {
                    case MsgID.Event_0:
                        EventMgr.GetInstance().InvokeEvent(tmp[1]);
                        break;
                    case MsgID.Event_1int:
                        EventMgr.GetInstance().InvokeEvent<int>(tmp[1], Convert.ToInt32(tmp[2]));
                        break;
                    case MsgID.Event_1string:
                        EventMgr.GetInstance().InvokeEvent<string>(tmp[1], tmp[2]);
                        break;
                    case MsgID.Event_1float:
                        EventMgr.GetInstance().InvokeEvent<float>(tmp[1], (float)Convert.ToDouble(tmp[2]));
                        break;
                    case MsgID.Rank:
                        for (int i = 1; i < 6; i++)
                        {
                            string[] parts = tmp[i].Split('/');
                            rank[i - 1].nameA = parts[0];
                            rank[i - 1].nameB = parts[1];
                            rank[i - 1].time = Convert.ToInt32(parts[2]);
                        }
                        hasReadRank = true;
                        break;
                    case MsgID.PlayerName:
                        GameManager.Instance.SetOtherPlayerName(tmp[1]);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                return;
            }
        }
        
        

    }
    #endregion
}


