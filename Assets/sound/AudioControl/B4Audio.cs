using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickBox;
    public AudioClip openBox;
    public AudioClip boxGetPieces;

    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BoxClick, ClickBox);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BoxOpen, OpenBox);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BoxGetPieces, BoxGetPieces);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BoxClick, ClickBox);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BoxOpen, OpenBox);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BoxGetPieces, BoxGetPieces);
    }
    private void ClickBox()
    {
        audioSource.clip= clickBox;
        audioSource.Play();
    }
    private void OpenBox()
    {
        audioSource.clip = openBox;
        audioSource.Play();
    }
    private void BoxGetPieces()
    {
        audioSource.clip = boxGetPieces;
        audioSource.Play();
    }

}
