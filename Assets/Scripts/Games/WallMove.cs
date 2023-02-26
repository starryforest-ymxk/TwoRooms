using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WallMove : MonoBehaviour
{
    public Vector3 position;
    public float duration;
    public float darkTime;
    public float endTime;
    public List<GameObject> teleportList = new List<GameObject>();
    private void Start()
    {
        if(GameManager.Instance.Triggers.Game_PuzzleGameCompleted)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.OnEndWallMove);
            Move();
            foreach(var a in teleportList)
            {
                a.SetActive(false);
            }
            Invoke("EndTrigger", endTime);
            Invoke("DarkTrigger", darkTime);
        }
    }

    private void Move()
    {
        gameObject.transform.DOMove(position, duration);
    }
    private void EndTrigger()
    {
        SceneMgr.Instance.TP(SceneManager.GetActiveScene().name, "End");
        EventMgr.GetInstance().InvokeEvent("OnGameEnd");
    }
    
    private void DarkTrigger()
    {
        EventMgr.GetInstance().InvokeEvent("BeforeGameEnd");
    }


}
