using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class button_click : MonoBehaviour
{
    public Text Num_red;
    public Text Num_green;
    public Text Num_blue;
    public Text Num_yellow;
    public Text Num_add;
    public List<GameObject> Addition = new List<GameObject>();
    public List<GameObject> Deletion = new List<GameObject>();

    private bool open = false;

    public void Click()
    {
        if (open)
            return;

        EventMgr.GetInstance().InvokeEvent(EventDic.Game_Coffin_ClickNum);

        if (((int)Num_add.text[0] - (int)'0') < 9)
        {
            Num_add.text = ((int)Num_add.text[0] - (int)'0' + 1).ToString();
        }
        else
        {
            Num_add.text = "1";
        }
        if((int)Num_red.text[0] - (int)'0' == 2 && (int)Num_green.text[0] - (int)'0' == 2 && (int)Num_blue.text[0] - (int)'0' == 3 && (int)Num_yellow.text[0] - (int)'0' == 1)
        {
            open = true;
            Invoke("OpenCoffin", 0.5f);
        }
    }

    private void OpenCoffin()
    {
        for (int i = 0; i < Addition.Count; i++)
        {
            Addition[i].SetActive(true);
        }
        for (int i = 0; i < Deletion.Count; i++)
        {
            Deletion[i].SetActive(false);
        }

        EventMgr.GetInstance().InvokeEvent(EventDic.Game_Coffin_IsDone);
    }
}
