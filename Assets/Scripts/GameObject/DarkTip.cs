using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTip : MonoBehaviour
{

    void Awake()
    {
        if (GameManager.Instance.Debugmode1)
        {
            GameManager.Instance.Triggers.Game_PBPushSun = true;
        }
        if (GameManager.Instance.Triggers.Game_PBPushSun)
            Destroy(gameObject);

        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPushSun, SelfDestroy);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPushSun, SelfDestroy);
    }
    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
