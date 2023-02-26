using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    private Image icon;
    private CanvasGroup descriptionGroup;
    private TMP_Text description;
    private TMP_Text textName;
    private ItemName itemName;
    private void Awake()
    {
        this.tag = "UIItem";
        icon = this.GetComponent<Image>();
        descriptionGroup = this.GetComponentInChildren<CanvasGroup>();
        var t = this.GetComponentsInChildren<TMP_Text>();
        textName = t[0];
        description = t[1];
    }
    public void SetItem(ItemDetail details)
    {
        this.icon.sprite = details.icon;
        this.itemName = details.name;
        this.description.text = details.description;
        this.textName.text = itemName.ToString();
        gameObject.GetComponent<RectTransform>().sizeDelta = details.sizeDelta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.DOKill();
        this.transform.DOScale(Settings.UIItemSelectScale, Settings.UIItemSelectTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.DOKill();
        this.transform.DOScale(Settings.UIItemDefaultScale, Settings.UIItemSelectTime);
    }

    public void OnDrop(PointerEventData eventData)
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnDropItem);
        GameManager.Instance.SetHoldingNothing();
        this?.transform.SetParent(GameManager.Instance.ItemBar.transform.Find("Viewport/ItemParent"));
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.Instance.SetHoldingItem(itemName);
        this.transform.SetParent(GameManager.Instance.MainCanvas.transform);
        this.transform.position = Input.mousePosition;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        switch (itemName)
        {
            case ItemName.地图:
                if (GameManager.Instance.MyPlayer == Player.p2)
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBShowMap);
                break;
            case ItemName.颜色提示:
                if (GameManager.Instance.MyPlayer == Player.p1)
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAShowColorClue);
                break;
            case ItemName.象形文字提示:
                if (GameManager.Instance.MyPlayer == Player.p2)
                    EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBShowSignClue);
                break;
            default:
                break;
        }
    }
}
