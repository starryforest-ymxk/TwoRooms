using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

public class MonoMgr : BaseMgr<MonoMgr>
{

    MonoController controller;
    public MonoMgr()
    {
        controller = MonoBehaviour.FindObjectOfType<MonoController>();    //将控制器绑定
    }
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);
    }
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }
    #region 协程的重写
    public Coroutine StartCoroutine(IEnumerator routine)  //协程的类型为IEnumerator，IEnumerator的作用即是保存当前运行状态然后退出
    {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    #endregion
}
