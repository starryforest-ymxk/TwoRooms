[System.Serializable]
public enum ItemName
{
    None,神像1,神像2,神像3,拼图碎片A,拼图碎片B1,拼图碎片B2,一片羽毛,绿色心脏,蓝色心脏,红色心脏,黄色心脏, 一把钥匙,地图,象形文字提示,颜色提示
}
/// <summary>
/// 游戏名或者是操作名，用于事件系统
/// </summary>
public enum GameName
{
    None, 按太阳,轮盘,华容道,p1冥府之船,
    壁画提示,开馆,心脏和手杖,手杖开箱,p2冥府之船,天平称重
}
public enum Player
{
    unSettle,p1, p2
}
/// <summary>
/// 玩家配对前，玩家配对后，玩家配对并确定角色后,游戏结束后
/// </summary>
public enum GameStage
{
    beforeMatch,afterMatch,inGame,AfterGame
}

public enum MsgID
{
    Event_0,Event_1int,Event_1string,Event_1float,
    GetRank,NewRank,Rank,PlayerName
}

public enum Heart
{ 
    None,Blue,Red,Green,Yellow
}
