using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BBalance : Interactive
{
    public bool Debug = false;

    [SerializeField] private Transform handle;
    [SerializeField] private float[] angles;
    [SerializeField] private SpriteRenderer[] hearts;
    private bool heartOn = false;
    protected override bool IsAvailiable => !GameManager.Instance.Triggers.Game_Balance_IsDone;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.Instance.Triggers.Game_Balance_IsDone) isDone = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BBalanceToNone, BalanceToNone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceFeather, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ABalanceToNone, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceIsDone, OpenMask);

        var heart = GameManager.Instance.Triggers.GetHeart();
        if (heart > 0)
        {
            hearts[(int)heart - 1].enabled = true;
            heartOn = true;
        }
        SetBalance(0, heart);
    }


    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BBalanceToNone, BalanceToNone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceFeather, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ABalanceToNone, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceIsDone, OpenMask);
    }
    public override void Interact()
    {
        if (!IsAvailiable || isDone) return;
        Heart heart = Heart.None;
        if (heartOn)
        {
            heartOn = false;
            foreach (var h in hearts)
            {
                if (h.enabled)
                {
                    h.enabled = false;
                    break;
                }
            }
            BagMgr.Instance.AddItem(GameManager.Instance.Triggers.GetHeartItem());
            EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BBalanceToNone);
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherBalanceIsNotDone);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePick);
        }
        if (GameManager.Instance.IsHolding(ItemName.绿色心脏) || GameManager.Instance.IsHolding(ItemName.红色心脏) || GameManager.Instance.IsHolding(ItemName.蓝色心脏) || GameManager.Instance.IsHolding(ItemName.黄色心脏))
        {
            switch (GameManager.Instance.HoldingItem)
            {
                case ItemName.绿色心脏:
                    heart = Heart.Green;
                    EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceGreenHeart);
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePut);
                    break;
                case ItemName.蓝色心脏:
                    heart = Heart.Blue;
                    EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceBlueHeart);
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePut);
                    break;
                case ItemName.红色心脏:
                    heart = Heart.Red;
                    EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceRedHeart);
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePut);
                    EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherBalanceIsDone);
                    CheckWin();
                    break;
                case ItemName.黄色心脏:
                    heart = Heart.Yellow;
                    EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceYellowHeart);
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePut);
                    break;
                default:
                    break;
            }
            heartOn = true;
            hearts[(int)heart - 1].enabled = true;
            BagMgr.Instance.DeleteDragingItem(GameManager.Instance.HoldingItem);
            SetBalance(1, heart);
        }
    }

    private void SetBalance(float time, Heart _h)
    {
        bool feather;
        try { feather = GameManager.Instance.Triggers.Game_PAPutFeather; }
        catch { feather = true; }
        if (!feather)
        {
            if (_h == Heart.None)
            {
                RotateHandle(1, time);
            }
            else
            {
                RotateHandle(0, time);
            }
        }
        else
        {

            switch (_h)
            {
                case Heart.None:
                    RotateHandle(2, time);
                    break;
                case Heart.Blue:
                    RotateHandle(2, time);
                    break;
                case Heart.Red:
                    RotateHandle(1, time);
                    break;
                case Heart.Green:
                    RotateHandle(0, time);
                    break;
                case Heart.Yellow:
                    RotateHandle(2, time);
                    break;
                default:
                    break;
            }
        }
    }

    private void BalanceToNone()
    {
        SetBalance(1, Heart.None);
    }
    private void RotateHandle(int index, float time)
    {
        if (index < 0 || index > 2) return;
        handle.DORotate(Vector3.forward * angles[index], time).SetEase(Ease.OutQuad);
        GameManager.Instance.Triggers.Game_PBBalanceAngle = angles[index];
    }
    //private void PutHeart(Heart h)
    //{
    //    int index = 0;
    //    index = (int)h - 1;
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (i == index) hearts[i].PutBack();
    //        else if (hearts[i].gameObject.activeSelf)
    //        {
    //            hearts[i].PickUp();
    //        }
    //    }
    //}
    private void UpdateBalance()
    {
        SetBalance(1, GameManager.Instance.Triggers.GetHeart());
    }

    private void CheckWin()
    {
        if (!GameManager.Instance.Triggers.Game_OtherBalance_IsDone) { return; }
        //TODO:完善游戏结束逻辑
        EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceIsDone);
        StartCoroutine(t());
        IEnumerator t()
        {
            yield return new WaitForSeconds(1.2f);
            SceneMgr.Instance.TP(SceneManager.GetActiveScene().name, "B2");
        }
    }

    public void OpenMask()
    {
        SceneMgr.Instance.TP(SceneManager.GetActiveScene().name, "B2");
    }

    [ContextMenu("Testing")]
    private void Testing()
    {
        Heart heart = Heart.None;
        for (int i = 0; i < 4; i++)
            if (hearts[i].gameObject.activeSelf)
            {
                heart = (Heart)i;
                break;
            }
        SetBalance(1, heart);
    }
}
