using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleGame : MonoBehaviour
{
    public Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private int[] slot = {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
    public List<int> APieces = new List<int> { 0, 1, 4, 5, 8, 9, 12, 13 };
    public List<int> BPieces = new List<int> { 2, 3, 6, 7, 10, 11, 14, 15 };
    public List<GameObject> slots;
    public List<GameObject> pieces;
    public float zoneHalfWidth;
    private bool myCompleted = false;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherPuzzleGameCompleted, OtherCompleted);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherPuzzleGameCompleted, OtherCompleted);
        EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherPuzzleGameNotCompleted);
    }
    public void OtherCompleted()
    {
        CheckWin();
    }
    public void SetPiece(int num, int pieceNum)
    {
        slot[num] = pieceNum;
        CheckCompleted();
    }
    public void PutBack(int num)
    {
        slot[num] = -1;
    }
    public void CheckCompleted()
    {
        bool completed = true;
        foreach(int i in GameManager.Instance.MyPlayer == Player.p1 ? APieces : BPieces)
        {
            if (slot[i] != i)
            {
                completed = false;
                break;
            }
        }
            myCompleted = completed;
        if (myCompleted)
        {
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherPuzzleGameCompleted);
            CheckWin();
        }
        else
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherPuzzleGameNotCompleted);


    }

    public void CheckWin()
    {
        if (GameManager.Instance.Triggers.Game_PuzzleGameOtherCompleted && myCompleted)
            Win();
    }

    public void Win()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PuzzleGameCompleted);
    }



}
