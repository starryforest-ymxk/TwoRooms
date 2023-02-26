using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3_B4ChangeBackground : MonoBehaviour
{
    private GameObject Stage1;
    private GameObject Stage2;
    private void Awake()
    {
        Stage1 = transform.GetChild(0).gameObject;
        Stage2 = transform.GetChild(1).gameObject;
    }
    void Start()
    {
        if(GameManager.Instance.Triggers.Game_NumGameCompleted)
        {
            Stage1.gameObject.SetActive(false);
            Stage2.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.Triggers.Game_ButtonGameCompleted)
        {
            Stage1.gameObject.SetActive(true);
            Stage2.gameObject.SetActive(false);
        }
        else
        {
            Stage1.gameObject.SetActive(false);
            Stage2.gameObject.SetActive(false);
        }

    }


}
