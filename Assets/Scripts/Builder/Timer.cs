using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoSingleton<Timer>
{
    private float time = 0f;
    public float currentTime => time;
    public float timeResult;
    public bool isTiming = false;
    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddEventListener("OnGameStart", () => StartTimer());
        EventMgr.GetInstance().AddEventListener("OnGameEnd", () => EndTimer());
        EventMgr.GetInstance().AddEventListener("OnGamePause", () => Pause());
        EventMgr.GetInstance().AddEventListener("OnGameResume", () => Resume());
    }

    private void FixedUpdate()
    {
        if(isTiming)
            time += Time.fixedDeltaTime;
    }

    private void StartTimer()
    {
        time = 0f;
        isTiming = true;
    }

    public void ResetTimer()
    {
        time = 0f;
        isTiming = false;
    }

    private void Pause()
    {
        isTiming = false;
    }

    private void Resume()
    {
        isTiming = true;
    }

    private void EndTimer()
    {
        isTiming = false;
        timeResult = time;
    }
}
