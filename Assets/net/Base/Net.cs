using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoSingleton<Net>
{
    Client client; 
    public string ipv4 = "";
    public int port;
    public string room;
    public string player;

    [HideInInspector] public bool connectToRoom = false;
    [HideInInspector] public bool getRoomSetResult = false;


    protected override void Awake()
    {
        base.Awake();
        client = gameObject.GetComponent<Client>();
    }

    private void SendInitMessage()
    {
        getRoomSetResult = false;
        client.Send($"Initialize:{room}:{player}");
    }


    public void InitClient()
    {
        client.SetInfo(room, player);
        SendInitMessage();
    }
    public void NetRoomError()
    {
        Debug.Log("房间已被占用，请重新输入房间号");
        connectToRoom = false;
        getRoomSetResult = true;
    }
    public void NetRoomConnected()
    {
        Debug.Log("房间连接成功");
        connectToRoom = true;
        getRoomSetResult = true;
        EventMgr.GetInstance().InvokeEvent<string>(EventDic.OnEnterRoom, room);
    }
    public void NetRoomReady()
    {
        Debug.Log("房间连接成功");
        connectToRoom = true;
        getRoomSetResult = true;
        client.roomReady = true;
        EventMgr.GetInstance().InvokeEvent<string>(EventDic.OnEnterRoom, room);
    }

    public void SetInfo(string ipv4, int port)
    {
        this.ipv4 = ipv4;
        this.port = port;
    }

    public void ResetGame()
    {
        connectToRoom = false;
        getRoomSetResult = false;
        room = "";
        player = "";
    }
}
