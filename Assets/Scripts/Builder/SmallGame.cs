using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有小游戏Manager的父类
/// </summary>
public abstract class SmallGame : MonoBehaviour
{
    [SerializeField] private GameName gameName;
    private void Awake()
    {
        this.gameObject.tag = "SmallGame";
    }
    public void WinGame(GameName gameName)
    {
        EventMgr.GetInstance().InvokeEvent<GameName>(EventDic.OnWinSmallGame, gameName);
    }
}
