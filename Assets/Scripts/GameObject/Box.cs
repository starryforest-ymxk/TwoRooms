using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Box : Interactive
{
    public Sprite close;
    public Sprite open;
    public Sprite openAndGet;
    protected override bool IsAvailiable => !GameManager.Instance.Triggers.Game_BoxGetPieces;

    private void Start()
    {
        if(GameManager.Instance.Triggers.Game_BoxGetPieces)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openAndGet;
        }
        else if (GameManager.Instance.Triggers.Game_BoxOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = open;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = close;
        }
    }

    public override void Interact()
    {
        if (!IsAvailiable) 
            return;
        if (!GameManager.Instance.Triggers.Game_BoxOpen && GameManager.Instance.IsHolding(ItemName.Ò»°ÑÔ¿³×))
        {
            BoxOpen();
        }
        else if(!GameManager.Instance.Triggers.Game_BoxOpen)
        {
            BoxShake();
        }
        else if(GameManager.Instance.Triggers.Game_BoxOpen && !GameManager.Instance.Triggers.Game_BoxGetPieces)
        {
            GetPieces();
        }
    }
    private void BoxOpen()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_BoxOpen);
        gameObject.GetComponent<SpriteRenderer>().sprite = open;
        BagMgr.Instance.DeleteDragingItem(ItemName.Ò»°ÑÔ¿³×);
    }
    private void GetPieces()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_BoxGetPieces);
        gameObject.GetComponent<SpriteRenderer>().sprite = openAndGet;
        BagMgr.Instance.AddItem(ItemName.Æ´Í¼ËéÆ¬B2);
    }
    private void BoxShake()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_BoxClick);
        transform.DOShakePosition(0.3f,0.2f,10,0,false,true);
    }

}
