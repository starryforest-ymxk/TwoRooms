using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndAnimation : MonoBehaviour
{
    public GameObject panel;
    public List<GameObject> list = new List<GameObject>();
    private void OnEnable()
    {
        EventMgr.GetInstance().AddEventListener(EventDic.BeforeGameEnd, Anm);
        EventMgr.GetInstance().AddEventListener(EventDic.OnEndWallMove, SetNotInteract);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.BeforeGameEnd, Anm);
        EventMgr.GetInstance().DeleteEventListener(EventDic.OnEndWallMove, SetNotInteract);
    }
    private void Anm()
    {
        panel.SetActive(true); 
        gameObject.GetComponent<Animator>().SetTrigger("End");
    }

    private void SetNotInteract()
    {
        foreach (var go in list)
        {
            go.SetActive(false);
        }
    }


}
