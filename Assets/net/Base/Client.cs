using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

public class Client : MonoSingleton<Client>
{

    private Socket socket;

    public bool connectToServer = false;
    public bool roomReady = false;

    private bool disconnected = false;
    private bool severOff = false;
    private bool otherExit = false;

    //发送，接受栈
    private byte[] receiveData = new byte[1024];
    private byte[] sendData = new byte[1024];

    private string ipv4;
    private int port;
    private string room;
    private string player;
    private float waitingTime = 0;
    private bool exit = false;

    //最新接收的一次消息
    private string message = "";
    public Queue<string> Messages => messages;

    private Queue<string> messages = new Queue<string>();


    public void SetInfo(string room, string player)
    {
        this.room = room;
        this.player = player;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize(string ipv4, int port)
    {
        

        this.ipv4 = ipv4;

        this.port = port;

        if (socket != null)
            socket.Close();

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipv4, port);

        StartReceive();
    }


    /// <summary>
    /// 客户端开始异步接收消息
    /// </summary>
    private void StartReceive()
    {
        socket.BeginReceive(receiveData, 0, receiveData.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }


    /// <summary>
    /// 接受消息的回调函数
    /// </summary>
    /// <param name="asyncResult"></param>
    private void ReceiveCallback(IAsyncResult asyncResult)
    {

        int len = socket.EndReceive(asyncResult);
        if (len == 0)
        {
            if(exit)
            {
                socket.Close();
                return;
            }
            else
            {
                Debug.Log("中断连接");
                waitingTime += 2;
                TryConnect();
                return;
            }

        }
        waitingTime = 0;
        message = Encoding.UTF8.GetString(receiveData, 0, len);        
        CheckCommand();

        StartReceive();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="info"></param>
    public void Send(string info)
    {
        sendData = Encoding.UTF8.GetBytes(info);
        socket.Send(sendData);
    }

    /// <summary>
    /// 重新连接
    /// </summary>
    public void TryConnect()
    {
        if(waitingTime >= Settings.ClientWaitingTime)
        {
            ClientDisconnected();
            return;
        }
        Debug.Log("尝试重新连接");
        socket.Close();
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(ipv4, port);
        }
        catch
        {
            ClientDisconnected();
            return;
        }
        StartReceive();
        Send($"Initialize:{room}:{player}");
    }
    public void ClientDisconnected()
    {
        Exit();
        port = 0;
        ipv4 = "";
        if (GameManager.Instance.GameStage != GameStage.beforeMatch)
            disconnected = true;
        Debug.Log("SeverDisconnected");
            
    }

    /// <summary>
    /// 客户端退出
    /// </summary>
    public void Exit()
    {
        try { Send($"Exit:"); } catch { }
        player = room = "";
        Net.instance.ResetGame();
        connectToServer = false;
        exit = true;
        socket.Close();
    }

    public void ExitRoom()
    {
        player = room = "";
        Net.instance.ResetGame();
        try { Send("ExitRoom:"); } 
        catch{}
    }

    /// <summary>
    /// 命令检索
    /// </summary>
    private void CheckCommand()
    {
        switch (message)
        {
            case "Server connected":
                connectToServer = true;
                break;
            case "Room error":
                Net.Instance.NetRoomError();
                break;
            case "Room connected":
                Net.Instance.NetRoomConnected();
                break;
            case "Room connected & ready":
                roomReady = true;
                Net.Instance.NetRoomReady();
                break;
            case "Server exit":
                socket.Close();
                connectToServer = false;
                Net.Instance.port = port = 0;
                Net.Instance.ipv4 = ipv4 = "";
                severOff = true;
                break;
            case "Other exit":
                ExitRoom();
                if (GameManager.Instance.GameStage == GameStage.afterMatch || GameManager.Instance.GameStage == GameStage.inGame)
                   otherExit = true;
                break;
            default:
                Messages.Enqueue(message);
                NetMsgMgr.Instance.readIt = true;
                break;
        }
    }

    private void Update()
    {
        if(disconnected)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.SeverDisconnected);
            disconnected = false;
        }
        else if(severOff)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.SeverOff);
            severOff = false;
        }
        else if(otherExit)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.OtherPlayerLeave);
            otherExit = false;
        }
    }

    public void OnApplicationQuit()
    {
        if(!GameManager.Instance.SceneDebugMode && connectToServer)
            Exit();
    }
}
