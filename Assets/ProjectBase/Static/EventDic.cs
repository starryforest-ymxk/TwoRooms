public static class EventDic
{
    #region 修改
    public const string SetItemCanPick = "SetItemCanPick";
    public const string SetItemCannotPick = "SetItemCannotPick";
    public const string SetCanClick = "SetCanClick";
    public const string SetCannotClick = "SetCannotClick";
    #endregion
    #region 网络事件
    public const string ReadMsg = "ReadMsg";
    public const string OtherPlayerLeave = "OtherPlayerLeave";
    public const string SeverOff = "SeverOff";
    public const string SeverDisconnected = "SeverDisconnected";
    public const string ServerMsg = "ServerMsg";

    #region 网络链接
    public const string P1ChosedByOther = "P1ChosedByOther";
    public const string P2ChosedByOther = "P2ChosedByOther";
    public const string P1ReleasedByOther = "P1ReleasedByOther";
    public const string P2ReleasedByOther = "P2ReleasedByOther";
    public const string PlayerConfirmed = "PlayerConfirmed";
    //public const string 
    #endregion
    #region 游戏事件


    public const string Game_PBShowMap = "玩家B展开地图";
    public const string Game_PAShowColorClue = "玩家B展开颜色线索";
    public const string Game_PBShowSignClue = "玩家B展开象形文字线索";
    public const string Game_CloseClue = "玩家收起线索";

    public const string Game_PBPushSunLocal = "玩家B在本地按下太阳";
    public const string Game_PBPushSun = "玩家B按下太阳";

    public const string Game_PARotateWheel = "玩家A转动轮盘";
    public const string Game_Wheel_IsDone = "轮盘游戏完成";

    public const string Game_ButtonGameCompleted = "按钮游戏已完成";
    public const string Game_ButtonGameOtherCompleted = "按钮游戏另一个玩家已完成";
    public const string Game_NumGameCompleted = "猜数字游戏已完成";
    public const string Game_NumGameCompletedStoneMove = "猜数字游戏完成打开墙面";
    public const string Game_PlayerClickButton = "玩家按下按钮";
    public const string Game_OtherPlayerClickButton = "另一个玩家按下按钮";
    public const string Game_ButtonPop = "按钮游戏提示";
    public const string Game_PAPutSign = "玩家A放置符号";
    public const string Game_PAPickSign = "玩家A取下符号";
    public const string Game_PAPut4Sign = "玩家A放置4个符号";
    public const string Game_PANotPut4Sign = "玩家A未放置4个符号";
    public const string Game_PAReset = "猜数字游戏重置";
    public const string Game_PBSignPop = "猜数字游戏弹出提示";

    public const string Game_PBMazeRotate = "玩家B转动某部分";
    public const string Game_PBMazeRotatePart1 = "顺时针转动玩家B转动部分1九十度";
    public const string Game_PBMazeRotatePart2 = "顺时针转动玩家B转动部分2九十度";
    public const string Game_PBMazeRotatePart3 = "顺时针转动玩家B转动部分3九十度";
    public const string Game_PBMazeRotatePart4 = "顺时针转动玩家B转动部分4九十度";
    public const string Game_PAMazeInPart1 = "玩家A在转动部分1";
    public const string Game_PAMazeInPart2 = "玩家A在转动部分2";
    public const string Game_PAMazeInPart3 = "玩家A在转动部分3";
    public const string Game_PAMazeInPart4 = "玩家A在转动部分4";
    public const string Game_Maze_Isdone = "迷宫谜题完成";

    public const string Game_BalancePut = "天平放置物品";
    public const string Game_BalancePick = "天平取下物品";
    public const string Game_BalanceFeather = "A放羽毛";
    public const string Game_BalanceRedHeart = "B放红心";
    public const string Game_BalanceBlueHeart = "B放蓝心";
    public const string Game_BalanceGreenHeart = "B放绿心";
    public const string Game_BalanceYellowHeart = "B放黄心";
    public const string Game_ABalanceToNone = "A天平变成空的";
    public const string Game_BBalanceToNone = "B天平变成空的";
    public const string Game_BalanceIsDone = "天平游戏完成";
    public const string Game_OtherBalanceIsDone = "另一方天平游戏完成";
    public const string Game_OtherBalanceIsNotDone = "另一方天平游戏没有完成";

    public const string Game_BoxClick = "点击箱子";
    public const string Game_BoxOpen = "箱子开";
    public const string Game_BoxGetPieces = "箱子开拿拼图";

    public const string Game_Coffin_ClickNum = "点击棺材数字";
    public const string Game_Coffin_IsDone = "打开棺材";

    public const string Game_PAPutIdol1 = "玩家A放入神像1槽位（不一定是正确神像）";
    public const string Game_PAPutIdol2 = "玩家A放入神像2槽位（不一定是正确神像）";
    public const string Game_PAPutIdol3 = "玩家A放入神像3槽位（不一定是正确神像）";
    public const string Game_PAPutIdol = "玩家A放入神像";
    public const string Game_PAPickIdol = "玩家A取出神像";
    public const string GamePaPutAllIdol = "玩家A放入所有神像";


    public const string Game_PlayerPutSign = "玩家放置象形文字";
    public const string Game_SignGame = "象形文字游戏完成";

    public const string Game_PAPutPuzzlePieces = "玩家A放置拼图碎片";
    public const string Game_PBPutPuzzlePieces1 = "玩家B放置拼图碎片1";
    public const string Game_PBPutPuzzlePieces2 = "玩家B放置拼图碎片2";
    public const string Game_PutPuzzlePieces = "玩家放置拼图";
    public const string Game_OtherPuzzleGameCompleted = "另一方拼图游戏完成";
    public const string Game_OtherPuzzleGameNotCompleted = "另一方拼图游戏没有完成";
    public const string Game_PuzzleGameCompleted = "拼图游戏完成";

    #endregion
    #endregion

    public const string OnClickUI = "OnClickUI";
    public const string UIpop = "UIpop";

    public const string GameResultEnterTop5 = "GameResultEnterTop5";
    public const string GameResult = "GameResult";

    public const string OnPick = "OnPick";
    public const string OnClickTeleport = "OnClickTeleport";
    public const string OnWinSmallGame = "OnWinSmallGame";
    public const string TryMatch = "TryMatch";
    public const string EndBeforeMatch = "EndBeforeMatch";
    public const string OnDropItem = "OnDropItem";
    public const string OnGameStart = "OnGameStart";
    public const string OnGameEnd = "OnGameEnd";
    public const string OnEnterRoom = "OnEnterRoom";
    public const string OnEndWallMove = "OnEndWallMove";
    public const string BeforeGameEnd = "BeforeGameEnd";


    public const string OnGamePause = "OnGamePause";
    public const string OnGameRestore = "OnGameResume";
    public const string GameDuration = "GameDuration";
    public const string EnterTop5 = "EnterTop5";
}
