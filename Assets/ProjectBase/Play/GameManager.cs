using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 游戏全局管理器，主要数据储存与读取
/// </summary>
public sealed class GameManager : MonoSingleton<GameManager>
{
    [Header("Debug开关")]
    [Tooltip("直接进入场景")] [SerializeField] private bool sceneDebugMode;
    [SerializeField] private string debugStartScene;
    [Tooltip("灯光自动开启")] [SerializeField] private bool debugmode1;
    [Tooltip("获取天平相关物品")] [SerializeField] private bool debugmode2;
    [Tooltip("获取拼图相关物品")] [SerializeField] private bool debugmode3;
    [Tooltip("获取祭台相关物品")][SerializeField] private bool debugmode4;
    [Tooltip("A2/B2直接触发结束动画")][SerializeField] private bool endDebug;
    [Header("游戏参数")]
    #region 重要引用    
    [SerializeField] private string p1StartScene;
    [SerializeField] private string p2StartScene;
    [SerializeField] private ItemName holdingItem;
    private GameObject itemBar;
    private string currentSceneName;
    private Triggers triggers = new Triggers();
    private List<ItemName> itemList = new List<ItemName>();
    private List<ItemName> hasEverGotItemList = new List<ItemName>();
    private Client client;
    private string roomName;
    private string playerName;
    private string otherPlayerName;
    private bool exception = false;

    public GameObject ItemBar => itemBar;
    public List<ItemName> ItemList => itemList;
    public List<ItemName> HasEverGotItemList => hasEverGotItemList;
    public string CurrentSceneName => currentSceneName;
    public Client Client => client;
    public Triggers Triggers => triggers;
    public string RoomName { get => roomName; set => roomName = value; }
    public string PlayerName => playerName;
    public string OtherPlayerName => otherPlayerName;
    public bool SceneDebugMode => sceneDebugMode;
    public bool Debugmode1 => debugmode1;
    public bool Debugmode2 => debugmode2;
    public bool Debugmode3 => debugmode3;
    public bool Debugmode4 => debugmode4;
    public bool EndDebug => endDebug;
    public GameObject MainCanvas => GameObject.FindGameObjectWithTag("MainCanvas");
    public ItemName HoldingItem => holdingItem;
    public string DebugStartScene => debugStartScene;

    [HideInInspector] public bool Exception { get => exception; set => exception = value; }
    #endregion
    #region 联机检测
    //TODO:检测对面的玩家，不能重复
    [SerializeField] private Player myPlayer;
    [SerializeField] private Player otherPlayer;
    [SerializeField] private GameStage gameStage = GameStage.beforeMatch;
    public Player MyPlayer => myPlayer;
    public Player OtherPlayer => otherPlayer;
    public GameStage GameStage => gameStage;






    #endregion
    #region 存档
    private PlayerData data;

    #endregion    
    protected override void Awake()
    {
        base.Awake();
        #region 重要引用
        itemBar = GameObject.FindGameObjectWithTag("ItemBar");
        client = GameObject.FindGameObjectWithTag("Net").GetComponent<Client>();
        #endregion  
        #region 事件注册
        EventMgr.GetInstance().AddEventListener(EventDic.OnGameEnd, () => { gameStage = GameStage.AfterGame; });
        #endregion 
    }

    private void Start()
    {
        data = new PlayerData();
        EventMgr.GetInstance().AddEventListener(EventDic.TryMatch, () => Client.Instance.Initialize(Net.Instance.ipv4, Net.Instance.port));
        //TODO:设定多个存档
        //TODO:选定角色后读取相应存档
        if (sceneDebugMode)
        {
            DebugLoadData();
            GameStart();
        }
    }
    #region 数据存储
    #region 调试
    /// <summary>
    /// 储存存档
    /// </summary>
    public void DebugSaveData()
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "SaveData"));
        string path = Path.Combine(Application.dataPath, "SaveData", "SaveData.loofah");
        //填充内容
        #region 填充数据
        data.myPlayer = this.myPlayer;
        data.SceneName = currentSceneName;
        data.items = ItemList;
        data.hasEverGotItems = this.HasEverGotItemList;
        data.TriggerJson = JsonUtility.ToJson(this.triggers);
        #endregion
        string js = JsonUtility.ToJson(data);
        File.WriteAllText(path, js, System.Text.Encoding.UTF8);
    }
    /// <summary>
    /// 读取存档
    /// </summary>
    public void DebugLoadData()
    {
        string path = Path.Combine(Application.dataPath, "SaveData", "SaveData.loofah");
        if (!Directory.Exists(Path.Combine(Application.dataPath, "SaveData")) || !File.Exists(path))
        {
            currentSceneName = p1StartScene;
            return;
        }
        try
        {
            string js = File.ReadAllText(path, System.Text.Encoding.UTF8);
            data = JsonUtility.FromJson<PlayerData>(js);
            #region 填充数据
            this.myPlayer = data.myPlayer;
            this.itemList = data.items;
            this.hasEverGotItemList = data.hasEverGotItems;
            BagMgr.Instance.HasEverGotItem = data.hasEverGotItems;
            currentSceneName = data.SceneName;
            this.triggers = JsonUtility.FromJson<Triggers>(data.TriggerJson);
            #endregion                        
        }
        catch
        {
            Debug.LogError("存档损坏");
            File.Move(path, Path.Combine(Application.dataPath, "SaveData", "CorruptedData.loofah"));
            File.Delete(path);
        }
    }
    #endregion
    #region 正式
    public void SaveData()
    {
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "SaveData"));
        string path = Path.Combine(Application.dataPath, "SaveData", myPlayer.ToString() + playerName + "SaveData.loofah");
        //填充内容
        #region 填充数据
        data.playerName = this.playerName;
        data.myPlayer = this.myPlayer;
        data.SceneName = currentSceneName;
        data.items = ItemList;
        data.hasEverGotItems = this.HasEverGotItemList;
        data.TriggerJson = JsonUtility.ToJson(this.triggers);
        #endregion
        string js = JsonUtility.ToJson(data);
        File.WriteAllText(path, js, System.Text.Encoding.UTF8);
    }
    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "SaveData", myPlayer.ToString() + playerName + "SaveData.loofah");
        if (!Directory.Exists(Path.Combine(Application.dataPath, "SaveData")) || !File.Exists(path))
        {
            switch (myPlayer)
            {
                case Player.p1:
                    currentSceneName = p1StartScene;
                    break;
                case Player.p2:
                    currentSceneName = p2StartScene;
                    break;
                default:
                    Debug.LogError("在未选定角色的时候载入存档");
                    break;
            }
            return;
        }
        try
        {
            string js = File.ReadAllText(path, System.Text.Encoding.UTF8);
            data = JsonUtility.FromJson<PlayerData>(js);
            #region 填充数据            
            this.myPlayer = data.myPlayer;
            this.itemList = data.items;
            this.hasEverGotItemList = data.hasEverGotItems;
            BagMgr.Instance.HasEverGotItem = data.hasEverGotItems;
            currentSceneName = data.SceneName;
            this.triggers = JsonUtility.FromJson<Triggers>(data.TriggerJson);
            #endregion                        
        }
        catch
        {
            Debug.LogError("存档损坏");
            File.Move(path, Path.Combine(Application.dataPath, "SaveData", "CorruptedData.loofah"));
            File.Delete(path);
        }
    }
    #endregion
    public void SetScene(string currentSceneName)
    {
        this.currentSceneName = currentSceneName;
    }
    public void SetPlayer(Player player)
    {
        //TODO:如何实现特定两个玩家对应一个存档
        this.myPlayer = player;
        if (myPlayer == Player.p1) otherPlayer = Player.p2;
        else if (myPlayer == Player.p2) otherPlayer = Player.p1;
    }
    public void SetPlayerName(string name)
    {
        this.playerName = name;
    }
    public void SetOtherPlayerName(string name)
    {
        this.otherPlayerName = name;
    }
    #endregion
    #region 联网
    private void netDetect()
    {
        switch (gameStage)
        {
            case GameStage.beforeMatch:
                break;
            case GameStage.afterMatch:
                break;
            case GameStage.inGame:
                break;
            default:
                break;
        }
    }
    #endregion
    #region 事件触发器
    public void GameStart()
    {
        //if (gameStage == GameStage.beforeMatch) Debug.LogError("提前开始游戏");
        //LoadData();
        //TODO:存档
        gameStage = GameStage.inGame;
        itemBar.GetComponent<CanvasGroup>().SetGroupOn();
        if (myPlayer == Player.p1) currentSceneName = p1StartScene;
        else currentSceneName = p2StartScene;
        if (!SceneDebugMode)
        {            
            Client.Instance.Send("GetName:");
        }
        else
        {
            myPlayer = debugStartScene[0] switch
            {
                'A' => Player.p1,
                'B' => Player.p2,
                _ => Player.p1
            };
        }
        EventMgr.GetInstance().InvokeEvent(EventDic.OnGameStart);

        //开局事件
        if (MyPlayer == Player.p2)
        {
            BagMgr.Instance.AddItem(ItemName.地图);
        }
    }
    public void EndBeforeMatch()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.EndBeforeMatch);
        gameStage = GameStage.afterMatch;
    }

    public void TryMatch()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.TryMatch);
    }
    #endregion
    #region 游戏状态
    public void SetHoldingItem(ItemName itemName)
    {
        holdingItem = itemName;
    }
    public void SetHoldingNothing()
    {
        holdingItem = ItemName.None;
    }
    public bool IsHolding(ItemName itemName)
    {
        return itemName == holdingItem;
    }
    #endregion    

    private void Update()
    {
        Debug.LogWarning(otherPlayerName);
    }
}
