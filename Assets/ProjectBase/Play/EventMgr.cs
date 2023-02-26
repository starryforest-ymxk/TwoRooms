using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo { }
public class EventInfo<T1, T2> : IEventInfo
{
    public UnityAction<T1, T2> Action;
}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> Action;
}
public class EventInfo : IEventInfo
{
    public UnityAction Action;
}
public class EventMgr : BaseMgr<EventMgr>
{
    /// <summary>
    /// 事件字典
    /// </summary>
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加无泛型事件观察者
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">触发函数</param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).Action += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo() { Action = action });
        }
    }
    /// <summary>
    /// 添加单泛型事件观察者
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">触发函数</param>
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).Action += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo<T>() { Action = action });
        }
    }
    public void AddEventListener<T1, T2>(string eventName, UnityAction<T1, T2> action)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T1, T2>).Action += action;
        }
        else
        {
            eventDic.Add(eventName, new EventInfo<T1, T2>() { Action = action });
        }
    }
    /// <summary>
    /// 删除无泛型事件观察者
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">删除函数</param>
    public void DeleteEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName) && eventDic[eventName] != null)
        {
            (eventDic[eventName] as EventInfo).Action -= action;
        }
        else
        {
            Debug.Log("尝试删除不存在的事件");
        }
    }
    /// <summary>
    /// 删除单泛型事件观察者
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="action">删除函数</param>
    public void DeleteEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName) && eventDic[eventName] != null)
        {
            (eventDic[eventName] as EventInfo<T>).Action -= action;
        }
        else
        {
            Debug.Log("尝试删除不存在的事件");
        }
    }
    public void DeleteEventListener<T1, T2>(string eventName, UnityAction<T1, T2> action)
    {
        if (eventDic.ContainsKey(eventName) && eventDic[eventName] != null)
        {
            (eventDic[eventName] as EventInfo<T1, T2>).Action -= action;
        }
        else
        {
            Debug.Log("尝试删除不存在的事件");
        }
    }
    /// <summary>
    /// 触发无泛型事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void InvokeEvent(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).Action?.Invoke();
        }
        else
        {
            Debug.LogWarning("尝试触发不存在的事件");
        }
    }
    public void InvokeTwoSideEvent(string eventName)
    {
        InvokeEvent(eventName);
        if(!GameManager.Instance.SceneDebugMode) NetMsgMgr.Instance.SendMsg(MsgID.Event_0, eventName);
    }

    public void InvokeOtherSideEvent(string eventName)
    {
        if (!GameManager.Instance.SceneDebugMode) NetMsgMgr.Instance.SendMsg(MsgID.Event_0, eventName);
    }
    /// <summary>
    /// 触发单泛型事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void InvokeEvent<T>(string eventName, T t)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).Action?.Invoke(t);
        }
        else
        {
            Debug.LogWarning("尝试触发不存在的事件");
        }
    }
    public void InvokeEvent<T1, T2>(string eventName, T1 t1, T2 t2)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T1, T2>).Action?.Invoke(t1, t2);
        }
        else
        {
            Debug.LogWarning("尝试触发不存在的事件");
        }
    }
    /// <summary>
    /// 清空事件观察者
    /// </summary>
    /// <param name="eventName">事件名称</param>
    public void ClearEventLinstener(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic.Remove(eventName);
        }
        else
        {
            Debug.LogWarning("尝试清空不存在的事件");
        }
    }
}
