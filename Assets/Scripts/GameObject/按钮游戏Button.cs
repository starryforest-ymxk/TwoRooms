using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 按钮游戏Button : Interactive
{
    private 按钮游戏 buttonGame;
    protected override bool IsAvailiable => !GameManager.Instance.Triggers.Game_ButtonGameCompleted;
    private Color color_down;
    private Color color_up;
    private SpriteRenderer sp;
    protected override void Awake()
    {
        base.Awake();
        sp = gameObject.GetComponent<SpriteRenderer>();
        buttonGame = gameObject.transform.parent.gameObject.GetComponent<按钮游戏>();
    }

    private void Start()
    {
        color_down = new Color(1f, 1f, 1f, 0f);
        color_up = new Color(1f, 1f, 1f, 1f);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            sp.color = color_up;
        }
    }

    public override void Interact()
    {
        if (!IsAvailiable) return;
        sp.color = color_down;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PlayerClickButton);
        buttonGame.MyPlayerClick();
    }
}
