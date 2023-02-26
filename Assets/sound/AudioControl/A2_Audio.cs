using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip wallMove;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.OnEndWallMove, WallMove);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnEndWallMove, WallMove);
    }

    public void WallMove()
    {
        audioSource.clip= wallMove;
        audioSource.Play();
    }
}
