using UnityEngine;
using UnityEngine.UI;
//TODO:确保两人都连接好后才能开始进行选择
public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private Player myPlayer;
    [SerializeField] private Player otherPlayer;
    [SerializeField] private Button p1Selector;
    [SerializeField] private Button p2Selector;
    [SerializeField] private Button p1Releaser;
    [SerializeField] private Button p2Releaser;
    [SerializeField] private Button readyConfirm;
    [SerializeField] private bool ready = false;
    [SerializeField] private bool otherReady = false;

    private bool canBeP1 = true;
    private bool canBeP2 = true;    
    private void Awake()
    {        
        EventMgr.GetInstance().AddEventListener(EventDic.P1ChosedByOther, P1Chosed);
        EventMgr.GetInstance().AddEventListener(EventDic.P2ChosedByOther, P2Chosed);
        EventMgr.GetInstance().AddEventListener(EventDic.P1ReleasedByOther, P1Released);
        EventMgr.GetInstance().AddEventListener(EventDic.P2ReleasedByOther, P2Released);
        EventMgr.GetInstance().AddEventListener(EventDic.PlayerConfirmed, PlayerConfirmed);
        readyConfirm.interactable = false;
        p1Releaser.gameObject.SetActive(false);
        p2Releaser.gameObject.SetActive(false);
        p1Selector.gameObject.SetActive(true);
        p2Selector.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.P1ChosedByOther, P1Chosed);
        EventMgr.GetInstance().DeleteEventListener(EventDic.P2ChosedByOther, P2Chosed);
        EventMgr.GetInstance().DeleteEventListener(EventDic.P1ReleasedByOther, P1Released);
        EventMgr.GetInstance().DeleteEventListener(EventDic.P2ReleasedByOther, P2Released);
        EventMgr.GetInstance().DeleteEventListener(EventDic.PlayerConfirmed, PlayerConfirmed);
    }
    private void StartGame()
    {
        canBeP1 = false;
        canBeP2 = false;
        ready = true;
        otherReady = true;
        GameManager.Instance.SetPlayer(myPlayer);
        GameManager.Instance.GameStart();        
        Debug.Log("开始游戏");
    }
    #region 按钮函数
    public void SetP1()
    {
        if (!canBeP1 || ready ||!Client.Instance.roomReady) return;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        ReleaseP2();
        p1Releaser.gameObject.SetActive(true);
        p1Selector.gameObject.SetActive(false);
        myPlayer = Player.p1;
        readyConfirm.interactable = true;
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.P1ChosedByOther);
    }
    public void ReleaseP1()
    {
        if (ready || !Client.Instance.roomReady) return;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        p1Releaser.gameObject.SetActive(false);
        p1Selector.gameObject.SetActive(true);
        myPlayer = Player.unSettle;
        readyConfirm.interactable = false;
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.P1ReleasedByOther);
    }
    public void SetP2()
    {
        if (!canBeP2 || ready|| !Client.Instance.roomReady) return;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        ReleaseP1();
        p2Releaser.gameObject.SetActive(true);
        p2Selector.gameObject.SetActive(false);
        myPlayer = Player.p2;
        readyConfirm.interactable = true;
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.P2ChosedByOther);
    }
    public void ReleaseP2()
    {
        if (ready || !Client.Instance.roomReady) return;
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        p2Releaser.gameObject.SetActive(false);
        p2Selector.gameObject.SetActive(true);
        myPlayer = Player.unSettle;
        readyConfirm.interactable = false;
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.P2ReleasedByOther);
    }
    #endregion
    public void GetReady()
    {
        readyConfirm.interactable = false;
        p1Selector.interactable = false;
        p2Selector.interactable = false;
        p1Releaser.interactable = false;
        p2Releaser.interactable = false;
        ready = true;
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.PlayerConfirmed);
        if (otherReady && ready) StartGame();
        //TODO:
        //else 显示等待对方准备
    }
    #region 网络接受信息函数
    private void P1Chosed()
    {
        canBeP1 = false;
        p1Selector.interactable = false;
        otherPlayer = Player.p1;
    }
    private void P2Chosed()
    {
        canBeP2 = false;
        p2Selector.interactable = false;
        otherPlayer = Player.p2;
    }
    private void P1Released()
    {
        canBeP1 = true;
        p1Selector.interactable = true;
        otherPlayer = Player.unSettle;
    }
    private void P2Released()
    {
        canBeP2 = true;
        p2Selector.interactable = true;
        otherPlayer = Player.unSettle;
    }
    private void PlayerConfirmed()
    {
        otherReady = true;
        if (otherReady && ready) StartGame();
        //TODO:
        //else 显示对方已准备
    }
    #endregion    
}
