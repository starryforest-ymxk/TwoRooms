using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRegister : MonoBehaviour
{
    private void OnEnable()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPushSun, Game_PBPushSun);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceFeather, Game_BalanceFeather);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceRedHeart, Game_BalanceRedHeart);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceBlueHeart, Game_BalanceBlueHeart);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceGreenHeart, Game_BalanceGreenHeart);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceYellowHeart, Game_BalanceYellowHeart);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ABalanceToNone, Game_ABalanceToNone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BBalanceToNone, Game_BBalanceToNone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceIsDone, Game_BalanceIsDone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherBalanceIsDone, Game_OtherBalanceIsDone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherBalanceIsNotDone, Game_OtherBalanceIsNotDone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_NumGameCompleted, Game_NumGameCompleted);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ButtonGameCompleted, Game_ButtonGameCompleted);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BoxOpen, Game_BoxOpen);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BoxGetPieces, Game_BoxGetPieces);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_SignGame, Game_SignGame);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAPutPuzzlePieces, Game_PAPutPuzzlePieces);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPutPuzzlePieces1, Game_PBPutPuzzlePieces1);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPutPuzzlePieces2, Game_PBPutPuzzlePieces2);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PuzzleGameCompleted, Game_PuzzleGameCompleted);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherPuzzleGameCompleted, OtherPuzzleGameCompleted);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherPuzzleGameNotCompleted, OtherPuzzleGameNotCompleted);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Coffin_IsDone, Game_Coffin_IsDone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, Game_Maze_Isdone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Wheel_IsDone, Game_Wheel_Isdone);
        EventMgr.GetInstance().AddEventListener<string>(EventDic.OnEnterRoom, OnEnterRoom);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPushSun, Game_PBPushSun);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceFeather, Game_BalanceFeather);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceRedHeart, Game_BalanceRedHeart);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceBlueHeart, Game_BalanceBlueHeart);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceGreenHeart, Game_BalanceGreenHeart);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceYellowHeart, Game_BalanceYellowHeart);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ABalanceToNone, Game_ABalanceToNone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BBalanceToNone, Game_BBalanceToNone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceIsDone, Game_BalanceIsDone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherBalanceIsDone, Game_OtherBalanceIsDone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherBalanceIsNotDone, Game_OtherBalanceIsNotDone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_NumGameCompleted, Game_NumGameCompleted);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ButtonGameCompleted, Game_ButtonGameCompleted);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BoxOpen, Game_BoxOpen);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BoxGetPieces, Game_BoxGetPieces);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_SignGame, Game_SignGame);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAPutPuzzlePieces, Game_PAPutPuzzlePieces);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPutPuzzlePieces1, Game_PBPutPuzzlePieces1);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPutPuzzlePieces2, Game_PBPutPuzzlePieces2);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PuzzleGameCompleted, Game_PuzzleGameCompleted);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherPuzzleGameCompleted, OtherPuzzleGameCompleted);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherPuzzleGameNotCompleted, OtherPuzzleGameNotCompleted);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Coffin_IsDone, Game_Coffin_IsDone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, Game_Maze_Isdone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Wheel_IsDone, Game_Wheel_Isdone);
        EventMgr.GetInstance().DeleteEventListener<string>(EventDic.OnEnterRoom, OnEnterRoom);
    }

    private void OnEnterRoom(string x)
    {
        GameManager.Instance.Triggers.EnterRoom = true;
        GameManager.Instance.RoomName = x;
    }

    public void Game_Wheel_Isdone()
    {
        GameManager.Instance.Triggers.Game_Wheel_IsDone = true;
    }

    private void Game_Maze_Isdone()
    {
        GameManager.Instance.Triggers.Game_Maze_IsDone = true;
    }

    private void Game_PBPushSun()
    {
        GameManager.Instance.Triggers.Game_PBPushSun = true;
    }
    private void Game_BalanceFeather()
    {
        GameManager.Instance.Triggers.Game_PAPutFeather = true;
    }
    private void Game_BalanceRedHeart()
    {
        GameManager.Instance.Triggers.PutHeart(Heart.Red);
    }
    private void Game_BalanceBlueHeart()
    {
        GameManager.Instance.Triggers.PutHeart(Heart.Blue);
    }
    private void Game_BalanceGreenHeart()
    {
        GameManager.Instance.Triggers.PutHeart(Heart.Green);
    }
    private void Game_BalanceYellowHeart()
    {
        GameManager.Instance.Triggers.PutHeart(Heart.Yellow);
    }
    private void Game_ABalanceToNone()
    {
        GameManager.Instance.Triggers.Game_PAPutFeather = false;
    }
    private void Game_BBalanceToNone()
    {
        GameManager.Instance.Triggers.PutHeart(Heart.None);
    }
    private void Game_BalanceIsDone()
    {
        GameManager.Instance.Triggers.Game_Balance_IsDone = true;
    }
    private void Game_OtherBalanceIsDone()
    {
        GameManager.Instance.Triggers.Game_OtherBalance_IsDone = true;
    }
    private void Game_OtherBalanceIsNotDone()
    {
        GameManager.Instance.Triggers.Game_OtherBalance_IsDone = false;
    }
    private void Game_Coffin_IsDone()
    {
        GameManager.Instance.Triggers.Game_Coffin_IsDone = true;
    }
    private void Game_NumGameCompleted()
    {
        GameManager.Instance.Triggers.Game_NumGameCompleted = true;
    }
    private void Game_ButtonGameCompleted()
    {
        GameManager.Instance.Triggers.Game_ButtonGameCompleted = true;
    }
    private void Game_BoxOpen()
    {
        GameManager.Instance.Triggers.Game_BoxOpen = true;
    }
    private void Game_BoxGetPieces()
    {
        GameManager.Instance.Triggers.Game_BoxGetPieces = true;
    }
    private void Game_SignGame()
    {
        GameManager.Instance.Triggers.Game_SignGame = true;
    }
    private void Game_PAPutPuzzlePieces()
    {
        GameManager.Instance.Triggers.Game_PAPutPuzzlePieces = true;
    }
    private void Game_PBPutPuzzlePieces1()
    {
        GameManager.Instance.Triggers.Game_PBPutPuzzlePieces1 = true;
    }
    private void Game_PBPutPuzzlePieces2()
    {
        GameManager.Instance.Triggers.Game_PBPutPuzzlePieces2 = true;
    }
    private void Game_PuzzleGameCompleted()
    {
        GameManager.Instance.Triggers.Game_PuzzleGameCompleted = true;
    }
    public void OtherPuzzleGameCompleted()
    {
        GameManager.Instance.Triggers.Game_PuzzleGameOtherCompleted = true;
    }
    public void OtherPuzzleGameNotCompleted()
    {
        GameManager.Instance.Triggers.Game_PuzzleGameOtherCompleted = false;
    }
}
