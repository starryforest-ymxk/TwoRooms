using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_B2_迷宫 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip rotate;
    public AudioClip openGate;

    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBMazeRotate, Rotate);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBMazeRotate, Rotate);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_Maze_Isdone, OpenGate);
    }

    public void Rotate()
    {
        audioSource.clip = rotate;
        audioSource.Play();
    }
    private void OpenGate()
    {
        audioSource.clip = openGate;
        audioSource.Play();
    }
}
