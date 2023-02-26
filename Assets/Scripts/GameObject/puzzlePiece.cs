using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePiece : MonoBehaviour
{
    private puzzleGame manager;
    public int pieceNum;
    public bool placed = false;

    private bool focus = false;
    private bool drag = false;

    public float originalScale;
    public float selectScale = 1.2f;
    public float selectTime = 0.05f;
    public float moveDuration1 = 0.2f;
    public float moveDuration2 = 0.4f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        manager = GameObject.Find("puzzleGame").GetComponent<puzzleGame>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        EventMgr.GetInstance().AddEventListener("拼图游戏完成", Win);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener("拼图游戏完成", Win);
    }
    private void Update()
    {
        if (focus && !drag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnBeginDrag();
                drag = true;
            }
        }
        else if (focus)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnDrop();
                focus = drag = false;
            }
        }
        if (drag)
        {
            transform.position = new Vector3(manager.mouseWorldPos.x, manager.mouseWorldPos.y, 0);
        }
    }
    public void OnBeginDrag()
    {
        transform.DOScale(originalScale, selectTime);
        spriteRenderer.sortingOrder = 2;
    }

    public void OnDrop()
    {
        spriteRenderer.sortingOrder = 1;
        foreach (var slot in manager.slots)
        {
            if (manager.mouseWorldPos.x < slot.transform.position.x + manager.zoneHalfWidth && manager.mouseWorldPos.x > slot.transform.position.x - manager.zoneHalfWidth
                && manager.mouseWorldPos.y < slot.transform.position.y + manager.zoneHalfWidth && manager.mouseWorldPos.y > slot.transform.position.y - manager.zoneHalfWidth)
            {
                slot.GetComponent<puzzleSlot>().SetPuzzle(gameObject);
            }
        }

    }
    
    public void OnMouseOver()
    {
        if (drag) return;
        transform.DOScale(selectScale * originalScale, selectTime);
        focus = true;
    }

    public void OnMouseExit()
    {
        if (drag) return;
        transform.DOScale(originalScale, selectTime);
        focus = false;
    }

    public void Move(Vector3 place)
    {
        transform.DOMove(place, moveDuration1);
    }
    private void Win()
    {
        gameObject.SetActive(false);
    }
}
