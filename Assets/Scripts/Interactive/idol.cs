using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idol : Interactive
{
    [SerializeField] private ItemName correctIdolName;
    [SerializeField] private Sprite[] idols;
    private int index;
    private ItemName currentIdolName = ItemName.None;
    private SpriteRenderer sp;
    protected override bool IsAvailiable => (GameManager.Instance.IsHolding(ItemName.None) || GameManager.Instance.IsHolding(ItemName.神像1) || GameManager.Instance.IsHolding(ItemName.神像2) || GameManager.Instance.IsHolding(ItemName.神像3))
        && !GameManager.Instance.Triggers.Game_IdolsDone;

    protected override void Awake()
    {
        base.Awake();
        sp = GetComponent<SpriteRenderer>();
        currentIdolName = correctIdolName switch
        {
            ItemName.神像1 => GameManager.Instance.Triggers.Game_PACurrentIdol1,
            ItemName.神像2 => GameManager.Instance.Triggers.Game_PACurrentIdol2,
            ItemName.神像3 => GameManager.Instance.Triggers.Game_PACurrentIdol3,
            _ => ItemName.None
        };
        sp.sprite = currentIdolName switch
        {
            ItemName.神像1 => idols[0],
            ItemName.神像2 => idols[1],
            ItemName.神像3 => idols[2],
            _ => null
        };
    }
    public override void Interact()
    {
        base.Interact();
        if (!IsAvailiable) return;
        var c = GameManager.Instance.HoldingItem;
        switch (c)
        {
            case ItemName.神像1:
                if (currentIdolName != ItemName.None) break;
                sp.sprite = idols[0];
                BagMgr.Instance.DeleteDragingItem(c);
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol);
                SetIdol(c);
                break;
            case ItemName.神像2:
                if (currentIdolName != ItemName.None) break;
                sp.sprite = idols[1];
                BagMgr.Instance.DeleteDragingItem(c);
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol);
                SetIdol(c);
                break;
            case ItemName.神像3:
                if (currentIdolName != ItemName.None) break;
                sp.sprite = idols[2];
                BagMgr.Instance.DeleteDragingItem(c);
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol);
                SetIdol(c);
                break;
            default:
                sp.sprite = null;
                if (currentIdolName != ItemName.None) BagMgr.Instance.AddItem(currentIdolName);
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPickIdol);
                SetIdol(c);
                break;
        }
        switch (correctIdolName)
        {
            case ItemName.神像1:
                GameManager.Instance.Triggers.Game_PAPutIdol1 = currentIdolName == correctIdolName;
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol1);
                break;
            case ItemName.神像2:
                GameManager.Instance.Triggers.Game_PAPutIdol2 = currentIdolName == correctIdolName;
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol2);
                break;
            case ItemName.神像3:
                GameManager.Instance.Triggers.Game_PAPutIdol3 = currentIdolName == correctIdolName;
                EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutIdol3);
                break;
        }
    }
    private void SetIdol(ItemName item)
    {
        switch (correctIdolName)
        {
            case ItemName.神像1:
                GameManager.Instance.Triggers.Game_PACurrentIdol1 = item;
                break;
            case ItemName.神像2:
                GameManager.Instance.Triggers.Game_PACurrentIdol2 = item;
                break;
            case ItemName.神像3:
                GameManager.Instance.Triggers.Game_PACurrentIdol3 = item;
                break;
            default:
                break;
        }
        currentIdolName = item;
    }
}
