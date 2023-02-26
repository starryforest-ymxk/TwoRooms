using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject setting_menu;
    public GameObject reloader;
    public GameObject SettingButton;

    private void OnEnable()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.OnGameStart, OnGameStart);
        EventMgr.GetInstance().AddEventListener(EventDic.OnEndWallMove, OnGameEnd);

    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnGameStart, OnGameStart);
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnEndWallMove, OnGameEnd);
    }


    public void OpenSetting()
    {
        if (GameManager.Instance.Exception)
            return;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        musicSlider.value = TemporaryData.MusicVolume;
        setting_menu.SetActive(true);
        GameManager.Instance.Exception = true;
    }

    public void SetAudioVolume()
    {
        float volume = musicSlider.value;
        TemporaryData.MusicVolume = volume;
        BGMMgr.Instance.SetVolume(volume);
    }

    public void BackToGame()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        GameManager.Instance.Exception = false;
        setting_menu.SetActive(false);
    }
    public void BackToMenu()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        GameManager.Instance.Exception = false;
        setting_menu.SetActive(false);
        Client.Instance.ExitRoom();
        var g = GameObject.Instantiate(reloader);
        g.GetComponent<Reloader>().SetType(false);
    }

    public void OnGameStart()
    {
        SettingButton.SetActive(true);
    }

    public void OnGameEnd()
    {
        SettingButton.SetActive(false);
    }

}
