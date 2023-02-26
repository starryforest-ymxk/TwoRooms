using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_concoller : MonoBehaviour
{
    public GameObject R1;
    public GameObject R2;  
    public GameObject R3;
    public GameObject R4;
    public GameObject P;
    private bool B1;
    private bool B2;    
    private bool B3;
    private bool B4;
    public List<GameObject> Addition = new List<GameObject>();
    public List<GameObject> Deletion = new List<GameObject>();
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBMazeRotatePart1, Rotate_maze1);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBMazeRotatePart2, Rotate_maze2);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBMazeRotatePart3, Rotate_maze3);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBMazeRotatePart4, Rotate_maze4);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart1, Rotate_set1);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart2, Rotate_set2);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart3, Rotate_set3);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart4, Rotate_set4);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, ChangeState);
        //监听转动消息
        B1 = true;
        B2 = true;
        B3 = true;
        B4 = true;
        //都可转动
        if (GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            P.transform.position = new Vector2(5.7f, 0.05f);
        }
    }
    private void Start()
    {
        ChangeState();
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBMazeRotatePart1, Rotate_maze1);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBMazeRotatePart2, Rotate_maze2);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBMazeRotatePart3, Rotate_maze3);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBMazeRotatePart4, Rotate_maze4);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart1, Rotate_set1);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart2, Rotate_set2);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart3, Rotate_set3);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart4, Rotate_set4);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, ChangeState);
    }
    private void Rotate_set1()
    {
        B1 = !B1;
    }
    private void Rotate_set2()
    {
        B2 = !B2;
    }
    private void Rotate_set3()
    {
        B3 = !B3;
    }
    private void Rotate_set4()
    {
        B4 = !B4;
    }
    //改变可转动状态
    private void Rotate_maze1()
    {
        if (B1) //若可转动
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBMazeRotate);
            R1.transform.Rotate(0, 0, 90);
        }
    }
    private void Rotate_maze2()
    {
        if (B2)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBMazeRotate);
            R2.transform.Rotate(0, 0, 90);
        }
    }
    private void Rotate_maze3()
    {
        if (B3)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBMazeRotate);
            R3.transform.Rotate(0, 0, 90);
        }
    }
    private void Rotate_maze4()
    {
        if (B4)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBMazeRotate);
            R4.transform.Rotate(0, 0, 90);
        }
    }

    public void ChangeState()
    {
        if (GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            foreach (var a in Addition)
            {
                if (a != null)
                {
                    a.SetActive(true);
                }
            }

            foreach (var a in Deletion)
            {
                if (a != null)
                {
                    a.SetActive(false);
                }
            }

        }

    }
}
