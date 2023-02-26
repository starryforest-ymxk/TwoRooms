using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ABalance : Interactive
{
    public bool Debug = false;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Transform handle;
    [SerializeField] private float[] angles;
    protected override bool IsAvailiable => !GameManager.Instance.Triggers.Game_Balance_IsDone;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.Instance.Triggers.Game_Balance_IsDone) isDone = true;
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ABalanceToNone, BalanceToNone);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceBlueHeart, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceRedHeart, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceYellowHeart, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceGreenHeart, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BBalanceToNone, UpdateBalance);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_BalanceIsDone, OpenMask);
        sp = transform.GetChild(1).GetComponent<SpriteRenderer>();

        bool isPut = GameManager.Instance.Triggers.Game_PAPutFeather;
        if (isPut) sp.enabled = true;
        SetBalance(0, isPut);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ABalanceToNone, BalanceToNone);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceBlueHeart, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceRedHeart, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceYellowHeart, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceGreenHeart, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BBalanceToNone, UpdateBalance);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_BalanceIsDone, OpenMask);
    }
    public override void Interact()
    {
        if (!IsAvailiable || isDone) return;
        if (sp.enabled)
        {
            sp.enabled = false;
            EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_ABalanceToNone);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePick);
            BagMgr.Instance.AddItem(ItemName.一片羽毛);
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherBalanceIsNotDone);
        }
        else if (GameManager.Instance.IsHolding(ItemName.一片羽毛))
        {
            sp.enabled = true;
            EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_BalanceFeather);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_BalancePut);
            BagMgr.Instance.DeleteDragingItem(ItemName.一片羽毛);
            SetBalance(1f, true);
            EventMgr.GetInstance().InvokeOtherSideEvent(EventDic.Game_OtherBalanceIsDone);
            CheckWin();
        }
    }
    private void BalanceToNone()
    {
        SetBalance(1f, false);
    }
    private void SetBalance(float time, bool feather)
    {
        Heart h = GameManager.Instance.Triggers.GetHeart();
        if (!feather)
        {
            if (h == Heart.None)
            {
                RotateHandle(1, time);
            }
            else
            {
                RotateHandle(2, time);
            }
        }
        else
        {

            switch (h)
            {
                case Heart.None:
                    RotateHandle(0, time);
                    break;
                case Heart.Blue:
                    RotateHandle(0, time);
                    break;
                case Heart.Red:
                    RotateHandle(1, time);
                    break;
                case Heart.Green:
                    RotateHandle(2, time);
                    break;
                case Heart.Yellow:
                    RotateHandle(0, time);
                    break;
                default:
                    break;
            }
        }
    }
    private void RotateHandle(int index, float time)
    {
        if (index < 0 || index > 2) return;
        handle.DORotate(Vector3.forward * angles[index], time).SetEase(Ease.OutQuad);
        GameManager.Instance.Triggers.Game_PABalanceAngle = angles[index];
    }
    /// <summary>
    /// 天平游戏结束内容
    /// </summary>    
    //private void PutFeather()
    //{
    //    Feather.PutBack();
    //}
    private void UpdateBalance()
    {
        SetBalance(1f, GameManager.Instance.Triggers.Game_PAPutFeather);
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
            SceneMgr.Instance.TP(SceneManager.GetActiveScene().name, "A2");
        }
    }
    public void OpenMask()
    {
        SceneMgr.Instance.TP(SceneManager.GetActiveScene().name, "A2");
    }

    #region 编辑器测试
    int index = 0;
    [ContextMenu("旋转")]
    private void rotate()
    {
        handle.DORotate(Vector3.forward * angles[(index++ % 3)], 1f).SetEase(Ease.OutQuad);
    }
    #endregion
}
