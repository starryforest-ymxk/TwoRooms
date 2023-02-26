using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private bool canPick;
    [SerializeField] private bool canPutBack;
    [SerializeField] private bool canPickAfterPutBack;
    [SerializeField] private ItemName itemName;
    public bool CanPick => canPick;
    private void OnEnable()
    {
        if (BagMgr.Instance.HasEverGotITem(itemName)) this.gameObject.SetActive(false);
        this.gameObject.tag = "Item";

        EventMgr.GetInstance().AddEventListener<ItemName>(EventDic.SetItemCanPick, x => SetCanPick(x));
        EventMgr.GetInstance().AddEventListener<ItemName>(EventDic.SetItemCannotPick, x => SetCannotPick(x));

        void SetCanPick(ItemName name)
        {
            if (!(name == itemName)) return;
            canPick = true;
        }
        void SetCannotPick(ItemName name)
        {
            if (!(name == itemName)) return;
            canPick = false;
        }
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener<ItemName>(EventDic.SetItemCanPick, x => SetCanPick(x));
        EventMgr.GetInstance().DeleteEventListener<ItemName>(EventDic.SetItemCannotPick, x => SetCannotPick(x));
        void SetCanPick(ItemName name)
        {
            if (!(name == itemName)) return;
            canPick = true;
        }
        void SetCannotPick(ItemName name)
        {
            if (!(name == itemName)) return;
            canPick = false;
        }
    }
    public void PickUp()
    {
        if (!canPick) return;
        PickAnm();
        EventMgr.GetInstance().InvokeEvent<ItemName>(EventDic.OnPick, itemName);
        BagMgr.Instance.AddItem(itemName);
    }
    public void PutBack()
    {
        if (!canPutBack) return;
        GameManager.Instance.SetHoldingItem(ItemName.None);
        if (BagMgr.Instance.ContainsItem(itemName))
        {
            BagMgr.Instance.DeleteDragingItem(itemName);
            BagMgr.Instance.HasEverGotItem.Remove(itemName);
        }
        gameObject.SetActive(true);
        canPick = canPickAfterPutBack;
    }
    private void PickAnm()
    {
        if (!canPutBack) Destroy(this.gameObject);
        else gameObject.SetActive(false);
    }
}
