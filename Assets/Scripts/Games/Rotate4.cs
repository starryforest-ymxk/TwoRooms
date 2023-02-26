using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate4 : MonoBehaviour
{
    private bool b4;
    private void Awake()
    {
        b4 = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart4, b4_set);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart4, b4_set);
    }
    public void Maze_rotate()
    {
        if (b4)
        {
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_PBMazeRotatePart4);//发送转动消息
        }
    }
    private void b4_set()
    {
        b4 = !b4;
    }
}
