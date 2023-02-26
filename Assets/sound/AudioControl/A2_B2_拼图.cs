using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_B2_拼图 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip putPieces;
    public AudioClip put;
    public AudioClip win;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutPuzzlePieces, PutPieces);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPutPuzzlePieces1, PutPieces);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPutPuzzlePieces2, PutPieces);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PutPuzzlePieces, Put);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PuzzleGameCompleted, Win);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutPuzzlePieces, PutPieces);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPutPuzzlePieces1, PutPieces);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPutPuzzlePieces2, PutPieces);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PutPuzzlePieces, Put);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PuzzleGameCompleted, Win);
    }
    private void PutPieces()
    {
        audioSource.clip = putPieces;
        audioSource.Play();
    }
    private void Put()
    {
        audioSource.clip = put;
        audioSource.Play();
    }
    private void Win()
    {
        audioSource.clip = win;
        audioSource.Play();
    }

}
