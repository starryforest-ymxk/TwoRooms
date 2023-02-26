using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBGM : MonoBehaviour
{
    public AudioSource BGMSource;
    public List<AudioClip> AudioClipList;
    private int topClip = 0;
    public AudioClip nextClip;
    public float nextClipTimer;
    public float firstTimeDuration;
    public float minTimeDuration;
    public float maxTimeDuration;
    public float minMusicDelay;
    public float maxMusicDelay;
    [SerializeField] private bool start = false;
    [SerializeField] private bool pause = false;


    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.OnClickTeleport, PlayBGM);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceIsDone, Pause);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnClickTeleport, PlayBGM);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceIsDone, Pause);
    }

    private void PlayBGM()
    {
        if (pause) 
            return;
        if(!start)
        {
            start= true;
            SetNextClip();
            nextClipTimer = firstTimeDuration;
        }
        else if (nextClipTimer == 0f)
        {
            BGMSource.clip = nextClip;
            SetNextClip();
            nextClipTimer = Random.Range(minTimeDuration, maxTimeDuration);
            Invoke("Play",Random.Range(minMusicDelay, maxMusicDelay));
        }
    }

    private void FixedUpdate()
    {
        if(start)
        {
            if (nextClipTimer > 0)
                nextClipTimer -= Time.fixedDeltaTime;
            else
                nextClipTimer = 0f;
        }
    }

    public void SetNextClip()
    {
        int index = Random.Range( 0, topClip + 1);
        nextClip= AudioClipList[index];
        if (index == topClip && topClip < AudioClipList.Count -1) 
            topClip++;
    }

    public void Play()
    {
        BGMSource.Play();
    }

    public void Pause()
    {
        pause = true;
        start = false;
        nextClipTimer = 0f;
    }
}
