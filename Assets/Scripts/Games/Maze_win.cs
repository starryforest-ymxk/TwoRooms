using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Maze_win : MonoBehaviour
{
    public float openDuration;
    public Vector3 targetPlace;
    public GameObject gate;
    public GameObject pickUp;

    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }
    private void Start()
    {
        if(GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            gate.SetActive(false);
            pickUp.SetActive(true);
        }
    }
    private void OpenGate()
    {
        gate.transform.DOMove(targetPlace,openDuration);
        pickUp.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Holder" && !GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_Maze_Isdone);
        }
    }
}
