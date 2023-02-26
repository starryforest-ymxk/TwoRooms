using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1drawer : MonoBehaviour
{
    [SerializeField] private GameObject prop;
    [SerializeField] private GameObject openedDoor;
    [SerializeField] private GameObject closedDoor;
    private void Awake()
    {
        bool open = GameManager.Instance.Triggers.Game_IdolsDone;
        openedDoor.SetActive(open);
        closedDoor.SetActive(!open);
        prop.SetActive(open);
    }
    private void OnEnable()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.GamePaPutAllIdol, Open);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.GamePaPutAllIdol, Open);
    }
    private void Open()
    {
        openedDoor.SetActive(true);
        closedDoor.SetActive(false);
        prop.SetActive(true);
    }
}
