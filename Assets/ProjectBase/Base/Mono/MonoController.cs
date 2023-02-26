using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoSingleton<MonoController>
{
    UnityAction updateAction = null;
    void Update()
    {
        if (updateAction != null)
        { updateAction(); }
    }
    override protected void Awake()
    {
        base.Awake();
    }
    public void AddUpdateListener(UnityAction func)
    {
        updateAction += func;
    }
    public void RemoveUpdateListener(UnityAction func)
    {
        updateAction -= func;
    }



}
