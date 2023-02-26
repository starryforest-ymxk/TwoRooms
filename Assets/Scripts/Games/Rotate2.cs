using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2 : MonoBehaviour
{
    private bool b2;
    private void Awake()
    {
        b2 = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart2, b2_set);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart2, b2_set);
    }
    public void Maze_rotate()
    {
        if (b2)
        {
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_PBMazeRotatePart2);//发送转动消息
        }
    }
    private void b2_set()
    {
        b2 = !b2;
    }
}
