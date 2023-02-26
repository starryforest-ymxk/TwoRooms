using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc_in : Interactive
{
    public GameObject P;
    public int stage = 1;
    public List<GameObject> Additions = new List<GameObject>();
    protected override bool IsAvailiable => true;

    protected override void Awake()
    {
        base.Awake();
        if(GameManager.Instance.Triggers.Game_Wheel_IsDone)
        {
            for (int i = 0; i < Additions.Count; i++)
            {
                Additions[i].SetActive(true);
            }
        }
    }
    public override void Interact()
    {
        base.Interact();
        if (stage <= 11)
        {
            stage++;
        }
        else
        {
            stage = 1;
        }
        switch (stage)
        {
            case 1:
                P.transform.Rotate(0, 0, 37);
                break;
            case 2:
                P.transform.Rotate(0, 0, 26);
                break;
            case 3:
                P.transform.Rotate(0, 0, 38);
                break;
            case 4:
                P.transform.Rotate(0, 0, 26);
                break;
            case 5:
                P.transform.Rotate(0, 0, 24);
                break;
            case 6:
                P.transform.Rotate(0, 0, 31);
                break;
            case 7:
                P.transform.Rotate(0, 0, 37);
                break;
            case 8:
                P.transform.Rotate(0, 0, 28);
                break;
            case 9:
                P.transform.Rotate(0, 0, 35);
                break;
            case 10:
                P.transform.Rotate(0, 0, 26);
                break;
            case 11:
                P.transform.Rotate(0, 0, 22);
                break;
            case 12:
                P.transform.Rotate(0, 0, 30);
                break;
        }
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PARotateWheel);
        if (GameObject.Find("Íâµã»÷Çø").GetComponent<PC_out>().stage == 3 && stage == 3 && !GameManager.Instance.Triggers.Game_Wheel_IsDone)
        {
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_Wheel_IsDone);
            for(int i= 0; i < Additions.Count; i++)
            {
                Additions[i].SetActive(true);
            }
        }
    }
}



