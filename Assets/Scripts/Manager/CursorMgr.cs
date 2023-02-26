using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMgr : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private Collider2D ObjectAtMouse => Physics2D.OverlapPoint(mouseWorldPos);
    private bool onObj;
    private bool canClick = true;
    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.SetCanClick, () => { canClick = true; });
        EventMgr.GetInstance().AddEventListener(EventDic.SetCannotClick, () => { canClick = false; });
        EventMgr.GetInstance().AddEventListener(EventDic.OnDropItem, DropAction);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnDropItem, DropAction);
    }
    private void Update()
    {
        onObj = ObjectAtMouse;
        if (onObj && canClick)
        {
            var g = ObjectAtMouse;
            SetCursor(g.tag);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ClickAction(g.gameObject);
            }
        }
    }
    private void ClickAction(GameObject g)
    {
        switch (g.tag)
        {
            case "Teleport":
                g.GetComponent<ClickTeleport>().TP();
                break;
            case "Item":
                g.GetComponent<Item>().PickUp();
                break;
            case "SmallGame":
                break;
            case "Interactive":
                g.GetComponent<Interactive>().Interact();
                break;
            case "UIItem":
                //g.GetComponent<UIItem>().ClickAction();
                break;
            default:
                Debug.LogWarning("未设定的碰撞体");
                break;
        }
    }
    private void DropAction()
    {
        if (ObjectAtMouse == null || ObjectAtMouse.tag != "Interactive") return;
        ObjectAtMouse.GetComponent<Interactive>().Interact();
    }
    private void SetCursor(string objectTag)
    {

    }
}
