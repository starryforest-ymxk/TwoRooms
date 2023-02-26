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
        controller = MonoBehaviour.FindObjectOfType<MonoController>();    //����������
    }
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);
    }
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }
    #region Э�̵���д
    public Coroutine StartCoroutine(IEnumerator routine)  //Э�̵�����ΪIEnumerator��IEnumerator�����ü��Ǳ��浱ǰ����״̬Ȼ���˳�
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
