using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoSingleton<SceneMgr>
{
    [SerializeField] private GameObject transitioner;
    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddEventListener(EventDic.OnGameStart, onGameStart);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnGameStart, onGameStart);
    }
    public void TP(string from, string to, float fadeTime = Settings.DefaultTransitionerTime)
    {
        var g = GameObject.Instantiate(transitioner);
        g.GetComponent<Transitioner>().Trans(from, to, fadeTime);
        GameManager.Instance.SetScene(to);
        //if (GameManager.Instance.DebugMode) GameManager.Instance.DebugSaveData();
        //else GameManager.Instance.SaveData();
        //TODO:上传
    }
    private void onGameStart()
    {
        string cScene = GameManager.Instance.CurrentSceneName;
#if UNITY_EDITOR
        if (GameManager.Instance.SceneDebugMode)
        {
            cScene = GameManager.Instance.DebugStartScene;
            TP(string.Empty, cScene);
        }
        else TP("Menu", cScene);
        return;
#endif
        TP("Menu", cScene);
    }
}
