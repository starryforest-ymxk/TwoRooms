using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(Collider2D))]
public class ClickTeleport : MonoBehaviour
{
    [SerializeField] private string from;
    [SerializeField] private string to;
    [Tooltip("在点亮祭台之前是否可以进行场景转换")]
    [SerializeField] private bool SwitchWithoutLight = true ;
    [SerializeField] private bool playAudio = false;
    private void Awake()
    {
        gameObject.tag = "Teleport";
    }
    public void TP()
    {
        if(( SwitchWithoutLight || GameManager.Instance.Triggers.Game_PBPushSun) && !GameManager.Instance.Exception)
        {
            SceneMgr.Instance.TP(from, to);
            if(playAudio)
                EventMgr.GetInstance().InvokeEvent(EventDic.OnClickTeleport);
        }
    }
}
