using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class Rotate1: MonoBehaviour
{
    private bool b1;
    public List<GameObject> Additions = new List<GameObject>();
    public List<GameObject> Deletions = new List<GameObject>();
    public GameObject gate;
    public float openDuration;
    public Vector3 targetPlace;

    private void Awake()
    {
        b1 = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAMazeInPart1, b1_set);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, ChangeState);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAMazeInPart1, b1_set);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, ChangeState);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }
    public void Maze_rotate()
    {
        if (b1)
        {
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_PBMazeRotatePart1);//发送转动消息
        }
    }
    private void b1_set()
    {
        b1 = !b1;
    }

    private void Start()
    {
        if (GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            gate.SetActive(false);
        }
    }
    //void Update()
    //{
    //    if (GameManager.Instance.Triggers.Game_Maze_IsDone)
    //    {
    //        for (int i = 0; i < 1; i++)
    //        {
    //            Additions[i].SetActive(true);
    //        }
    //        for (int i = 0; i < Deletions.Count; i++)
    //        {
    //            Deletions[i].SetActive(false);
    //        }
    //    }
    //    if(GameManager.Instance.Triggers.Game_SignGame)
    //    {
    //        for(int i = 1;i<Additions.Count;i++)
    //        {
    //            Additions[i].SetActive(true);
    //        }
    //    }
    //}


    public void ChangeState()
    {
        if (GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            foreach (var a in Additions)
            {
                if (a != null)
                {
                    a.SetActive(true);
                }
            }

            foreach (var a in Deletions)
            {
                if (a != null)
                {
                    a.SetActive(false);
                }
            }

        }
    }

    private void OpenGate()
    {
        gate.transform.DOMove(targetPlace, openDuration);
    }
}
