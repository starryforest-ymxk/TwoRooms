using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSlot:MonoBehaviour
{
    public int num;
    private bool isOccupied = false;
    private CharacterGame manager;
    private GameObject currentCharacter;

    private void Start()
    {
        manager = GameObject.Find("CharacterGame").GetComponent<CharacterGame>();
    }

    public void SetCharacter(GameObject character)
    {
        if (isOccupied) return;
        Debug.Log("setCharacter");
        isOccupied = true;
        character.GetComponent<Character>().Move(transform.position);
        character.transform.SetParent(this.transform);
        manager.SetCharacter(num, character.GetComponent<Character>().characterNum);
        character.GetComponent<Character>().placed = true;
        character.GetComponent<SpriteRenderer>().sortingOrder = character.GetComponent<Character>().putOrder;
        currentCharacter = character;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PlayerPutSign);

    }
    public void PickUp()
    {
        if (!isOccupied) return;
        Debug.Log("PickUp");
        isOccupied = false;
        currentCharacter.transform.SetParent(null);
        manager.PutBack(num);
        currentCharacter.GetComponent<Character>().placed = false;
        currentCharacter.GetComponent<SpriteRenderer>().sortingOrder = currentCharacter.GetComponent<Character>().originalOrder;
        currentCharacter = null;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isOccupied
            && manager.mouseWorldPos.x < transform.position.x + manager.zoneHalfWidth && manager.mouseWorldPos.x > transform.position.x - manager.zoneHalfWidth
                && manager.mouseWorldPos.y < transform.position.y + manager.zoneHalfWidth && manager.mouseWorldPos.y > transform.position.y - manager.zoneHalfWidth)
        {
            PickUp();
        }
    }
}
