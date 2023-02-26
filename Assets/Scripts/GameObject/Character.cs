using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Character : MonoBehaviour
{

    private CharacterGame manager;
    private SpriteRenderer spriteRenderer;
    public int characterNum;
    public bool placed = false;

    private bool focus = false;
    private bool drag = false;

    public float originalScale = 1f;
    public float selectScale = 1.3f;
    public float selectTime = 0.05f;
    public int originalOrder = 3;
    public int selectOrder = 4;
    public int putOrder = 5;
    public float moveDuration1 = 0.2f;
    public float moveDuration2 = 0.4f;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        manager = GameObject.Find("CharacterGame").GetComponent<CharacterGame>();
    }
    private void Update()
    {

        if (focus && !drag)
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnBeginDrag();
                drag = true;
            }
        }
        else if(focus)
        {
            if(Input.GetMouseButtonUp(0))
            {
                focus = drag = false ;
                OnDrop();
            }
        }
        if(drag)
        {
            transform.position = new Vector3(manager.mouseWorldPos.x, manager.mouseWorldPos.y,0);
        }
    }
    public void OnBeginDrag()
    {
        transform.DOScale(originalScale, selectTime);
        spriteRenderer.sortingOrder = selectOrder;
    }

    public void OnDrop()
    {
        spriteRenderer.sortingOrder = originalOrder;
        foreach (var slot in manager.slots)
        {
            if(manager.mouseWorldPos.x < slot.transform.position.x + manager.zoneHalfWidth && manager.mouseWorldPos.x > slot.transform.position.x - manager.zoneHalfWidth
                && manager.mouseWorldPos.y < slot.transform.position.y + manager.zoneHalfWidth && manager.mouseWorldPos.y > slot.transform.position.y - manager.zoneHalfWidth)
            {
                slot.GetComponent<CharacterSlot>().SetCharacter(gameObject);
            }
        }

    }

    public void OnMouseOver()
    {
        if (drag) return;
        transform.DOScale(selectScale, selectTime);
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
        transform.DORotate(Vector3.zero, moveDuration1);
    }
}
