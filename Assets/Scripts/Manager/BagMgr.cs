using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMgr : MonoSingleton<BagMgr>
{
    [SerializeField] private ItemList allItem;
    [SerializeField] private List<ItemName> currentItem;
    [SerializeField] private List<ItemName> hasEverGotItem;
    [SerializeField] private GameObject uIItem;

    public List<ItemName> CurrentItem { get => currentItem; set => currentItem = value; }
    public List<ItemName> HasEverGotItem { get => hasEverGotItem; set => hasEverGotItem = value; }

    //private List<ItemName> currentItem;
    protected override void Awake()
    {
        base.Awake();
        EventMgr.GetInstance().AddEventListener(EventDic.OnGameStart, onGameStart);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnGameStart, onGameStart);
    }
    private void onGameStart()
    {
        var list = GameManager.Instance.ItemList;
        for (int i = 0; i < list.Count; i++) AddItem(list[i]);
    }
    public void AddItem(ItemName itemName)
    {
        if (currentItem.Contains(itemName)) return;
        if (!hasEverGotItem.Contains(itemName))
        {
            hasEverGotItem.Add(itemName);
            GameManager.Instance.HasEverGotItemList.Add(itemName);
        }
        ItemDetail t = allItem.GetItem(itemName);
        if (t == null)
        {
            Debug.LogError("错误的物品名");
            return;
        }
        currentItem.Add(itemName);
        GameManager.Instance.ItemList.Add(itemName);

        addItemToUI(t);

        void addItemToUI(ItemDetail detail)
        {
            Transform t = GameManager.Instance.ItemBar.transform.Find("Viewport/ItemParent");
            GameObject g = GameObject.Instantiate(uIItem);
            g.GetComponent<UIItem>().SetItem(detail);
            g.name = detail.name.ToString();
            g.transform.SetParent(t);
            g.transform.localScale = Vector3.one;
        }
    }

    public void DeleteItem(ItemName itemName)
    {
        if (currentItem.Contains(itemName))
        {
            currentItem.Remove(itemName);
            GameManager.Instance.ItemList.Remove(itemName);
            Transform t = GameManager.Instance.ItemBar.transform.GetChild(0).GetChild(0).transform;
            GameObject g = t.transform.Find(itemName.ToString()).gameObject;
            if (g == null) Debug.LogError("尝试删除未获得的UIItem");
            else Destroy(g);
        }
        else Debug.LogError("尝试删除未获得/不存在的物品:" + itemName);
    }
    public void DeleteDragingItem(ItemName itemName)
    {
        if (currentItem.Contains(itemName))
        {
            currentItem.Remove(itemName);
            GameManager.Instance.ItemList.Remove(itemName);
            Transform t = GameManager.Instance.ItemBar.transform.parent;
            GameObject g = t.GetChild(t.transform.childCount-1).gameObject;
            if (g == null) Debug.LogError("尝试删除未获得的UIItem");
            else Destroy(g);
        }
        else Debug.LogError("尝试删除未获得/不存在的物品:" + itemName);
    }
    public bool ContainsItem(ItemName itemName) => currentItem.Contains(itemName);
    public bool HasEverGotITem(ItemName itemName) => hasEverGotItem.Contains(itemName);
}
