using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3 : MonoBehaviour
{
    private bool b3;
    private void Awake()
    {
        b3 = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart3, b3_set);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart3, b3_set);
    }
    public void Maze_rotate()
    {
        if (b3)
        {
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_PBMazeRotatePart3);//发送转动消息
        }
    }
    private void b3_set()
    {
        b3 = !b3;
    }
}
