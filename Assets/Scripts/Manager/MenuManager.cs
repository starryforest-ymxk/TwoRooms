using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup net_menu;
    [SerializeField] private CanvasGroup music_menu;
    [SerializeField] private CanvasGroup tip;
    [SerializeField] private CanvasGroup warning;
    [SerializeField] private CanvasGroup roomMenu;
    [SerializeField] private CanvasGroup roomWarning;
    [SerializeField] private CanvasGroup character_menu;

    [SerializeField] private Button startButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button netSetButton;
    [SerializeField] private Button rankButton;

    [SerializeField] private TMP_InputField input_ipv4;
    [SerializeField] private TMP_InputField input_port;
    [SerializeField] private TMP_InputField input_room;
    [SerializeField] private TMP_InputField input_name;

    public string ipv4 = "";
    public int port = 0;

    private bool setNet = false;
    private bool openTip = false;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        input_ipv4.text = "";
        input_port.text = "";
        input_room.text = "";
        input_name.text = "";
    }
    public void StartGame()
    {

        CloseMenu();
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        if (setNet || Client.Instance.connectToServer)
        {

            if (Client.Instance.connectToServer)
                OpenRoomMenu();  //如果成功连接，就打开“加入或创建房间”菜单
            else
                OpenWarning();  //如果连接失败，就OpenWarning

        }
        else
        {
            OpenTip();
        }

    }

    public void Exit()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        Application.Quit();
    }

    public void OpenNetMenu()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        if (Net.Instance.ipv4 != "")
            input_ipv4.text = Net.Instance.ipv4;

        if (Net.Instance.port != 0)
            input_port.text = Net.Instance.port.ToString();

        CloseMenu();
        net_menu.SetGroupOn();
    }

    public void SetNet()
    {

        bool NetChange = false ;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        try
        {
            if (input_ipv4.text != "")
            {
                ipv4 = input_ipv4.text;
                setNet = true;
            }
            if (input_port.text != "")
            {
                port = Convert.ToInt32(input_port.text);
                setNet = true;
            }
        }
        catch
        {
            Debug.LogWarning("输入错误");
        }

        if (!setNet)
        {
            net_menu.SetGroupOff();
            return;
        }
        

        if(Net.Instance.ipv4 != ipv4 || Net.Instance.port != port)
        {
            NetChange = true;
            Net.Instance.SetInfo(ipv4, port);
        }

        if(!Client.Instance.connectToServer || NetChange)
        {
            try
            {
                GameManager.Instance.TryMatch();
            }
            catch { }
            Invoke("CheckConnect", Settings.NetDelayTime); 
        }
        net_menu.SetGroupOff();

    }

    public void OpenMusicMenu()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        CloseMenu();
        musicSlider.value = TemporaryData.MusicVolume;
        music_menu.SetGroupOn();
    }

    public void OpenTip()
    {
        CloseMenu();
        openTip = true;
        EventMgr.GetInstance().InvokeEvent(EventDic.UIpop);
        tip.SetGroupOn();
    }

    public void OpenWarning()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.UIpop);
        CloseMenu();
        warning.SetGroupOn();
    }

    public void CloseMenu()
    {
        roomMenu.SetGroupOff();
        roomWarning.SetGroupOff();
        net_menu.SetGroupOff();
        music_menu.SetGroupOff();
        tip.SetGroupOff();
        warning.SetGroupOff();
        if(openTip)
        {
            openTip = false;
            net_menu.SetGroupOn();
        }
    }

    public void OpenRoomMenu()
    {
        roomWarning.SetGroupOff();
        roomMenu.SetGroupOn();
    }

    public void OpenRoomWarning()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.UIpop);
        roomMenu.SetGroupOff();
        roomWarning.SetGroupOn();
    }

    public void SetRoomAndName()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        try
        {
            if (input_room.text != "")
                Net.Instance.room = input_room.text;
            if (input_name.text != "")
                Net.Instance.player = input_name.text;
        }
        catch
        {
            Debug.LogWarning("输入错误");
        }


        if (Net.Instance.room.Length >= 1 && Net.Instance.player.Length >= 1)
        {
            GameManager.Instance.SetPlayerName(Net.Instance.player);

            Net.Instance.InitClient();

            Invoke("WaitReply", Settings.NetDelayTime);
        }
        roomMenu.SetGroupOff();

    }

    private void WaitReply()
    {
        if(!Net.Instance.getRoomSetResult)
        {
            Invoke("WaitReply", Settings.NetDelayTime);
            return;
        }
        if (!Net.Instance.connectToRoom)
        {
            OpenRoomWarning();
        }
        else
        {
            CloseMenu();
            startButton.interactable = false;
            musicButton.interactable = false;
            netSetButton.interactable = false;
            rankButton.interactable = false;

            SetCharacter();

        }
    }

    public void CheckConnect()
    {
        if (!Client.Instance.connectToServer)
            OpenWarning();  //如果连接失败，就OpenWarning
        else
        {
            GameManager.Instance.EndBeforeMatch();
        }
    }


    public void CheckRank()
    {
        CloseMenu();
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        if (setNet || Client.Instance.connectToServer)
        {
            if (Client.Instance.connectToServer)
            {
                if(!NetMsgMgr.Instance.hasReadRank)
                    Client.Instance.Send("GetRank:");
                SceneManager.LoadScene("Rank", LoadSceneMode.Additive);
            }
            else
                OpenWarning();  //如果连接失败，就OpenWarning

        }
        else
        {
            OpenTip();
        }
    }




    public void SetCharacter()
    {
        character_menu.SetGroupOn();
    }
    public void SetAudioVolume()
    {
        float volume = musicSlider.value;
        TemporaryData.MusicVolume = volume;
        BGMMgr.Instance.SetVolume(volume);
    }
    public void Confirm()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        CloseMenu();
    }

    public void Click()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
    }

    public void CheckInfo()
    {
        CloseMenu();
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        SceneManager.LoadScene("Menu_Acknowledgements", LoadSceneMode.Additive);
    }

}
