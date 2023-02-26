using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1祭坛 : MonoBehaviour
{    
    private void OnEnable()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutIdol1, UpdateState);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutIdol2, UpdateState);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutIdol3, UpdateState);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutIdol1, UpdateState);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutIdol2, UpdateState);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutIdol3, UpdateState);
    }

    private void UpdateState()
    {
        var t = GameManager.Instance.Triggers;
        bool flag = t.Game_PAPutIdol1 && t.Game_PAPutIdol2 && t.Game_PAPutIdol3;
        if (flag)
        {
            t.Game_IdolsDone = true;
            Invoke("IdolDone", 0.5f);
        }
    }

    public void IdolDone()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.GamePaPutAllIdol);
    }
}
