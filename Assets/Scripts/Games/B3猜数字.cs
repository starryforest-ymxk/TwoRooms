using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class B3猜数字 : MonoBehaviour
{

    public GameObject ob1;
    public GameObject ob2;
    private List<GameObject> obList = new List<GameObject>();
    private bool displayTips = false;
    private bool unDisplay = false;
    private bool reset = false;
    private string message;

    private void Awake()
    {
        EventMgr.GetInstance().AddEventListener<string>(EventDic.Game_PAPut4Sign, Game_PAPut4Sign);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PANotPut4Sign, Game_PANotPut4Sign);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAReset, Game_PAReset);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener<string>(EventDic.Game_PAPut4Sign, Game_PAPut4Sign);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PANotPut4Sign, Game_PANotPut4Sign);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAReset, Game_PAReset);
    }
    private void Game_PAPut4Sign(string x)
    {
        displayTips = true; 
        message = x;
    }

    private void Game_PANotPut4Sign()
    {
        unDisplay = true;
    }
    private void Game_PAReset()
    {
        reset = true;
    }


    private void Start()
    {
        Entry(); 
    }

    private void Update()
    {
        if (displayTips)
        {
            DisplayTips(message);
            displayTips = false;
            message = "";
        }
        if (unDisplay)
        {
            UnDisplay();
            unDisplay = false;
        }
        if(reset)
        {
            StartCoroutine(Reset());
            reset = false;
        }

    }


    private void DisplayTips(string message)
    {
        int correctnum = 0;
        int wrongplace = 0;
        string[] num = message.Split('~');
        Debug.Log(message);
        if(num.Length == 2)
        {
            try
            {
                correctnum = Convert.ToInt32(num[0]);
                wrongplace = Convert.ToInt32(num[1]);
            }
            catch { }
        }

        if(obList.Count > 0)
        {
            foreach(GameObject go in obList)
            {
                Destroy(go);
                Debug.Log("Destroy");
            }
            obList.Clear();

        }

        for(int i = 0; i < correctnum; i++)
        {
            Debug.Log(correctnum);
            GameObject gb = Instantiate(ob1, transform.GetChild(i));
            gb.transform.localPosition = new Vector3(0, 0, 0);
            obList.Add(gb);
        }

        for (int i = 0; i < wrongplace; i++)
        {
            Debug.Log(wrongplace);
            GameObject gb = Instantiate(ob2, transform.GetChild(correctnum + i));
            gb.transform.localPosition = new Vector3(0, 0, 0);
            obList.Add(gb);
        }

        if (correctnum != 0 || wrongplace != 0)
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);

        if (correctnum == 4) Win();
    }

    private void UnDisplay()
    {
        Debug.Log("Destroy");

        if (obList.Count > 0)
        {
            foreach (GameObject go in obList)
            {
                Destroy(go);
            }
            obList.Clear();
        }
    }



    private IEnumerator Reset()
    {
        if (obList.Count > 0)
        {
            foreach (GameObject go in obList)
            {
                Destroy(go);
            }
            obList.Clear();
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject gb = Instantiate(ob2, transform.GetChild(i));
            gb.transform.localPosition = new Vector3(0, 0, 0);
            obList.Add(gb);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);
        }
        yield return new WaitForSeconds(1f);

        foreach (GameObject go in obList)
            go.SetActive(false);
        yield return new WaitForSeconds(0.7f);

        foreach (GameObject go in obList)
            go.SetActive(true);
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);
        yield return new WaitForSeconds(1f);

        foreach (GameObject go in obList)
            Destroy(go);
        obList.Clear();

    }

    private void Win()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_NumGameCompleted);
        StartCoroutine(AfterWin());
    }

    private IEnumerator AfterWin()
    {
        if (obList.Count > 0)
        {
            foreach (GameObject go in obList)
            {
                Destroy(go);
            }
            obList.Clear();
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject gb = Instantiate(ob1, transform.GetChild(i));
            gb.transform.localPosition = new Vector3(0, 0, 0);
            obList.Add(gb);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);
        }
        yield return new WaitForSeconds(1f);

        foreach (GameObject go in obList)
            go.SetActive(false);
        yield return new WaitForSeconds(0.7f);

        foreach (GameObject go in obList)
            go.SetActive(true);
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);
        yield return new WaitForSeconds(1f);

        foreach (GameObject go in obList)
            go.SetActive(false);
        yield return new WaitForSeconds(0.7f);

        foreach (GameObject go in obList)
            go.SetActive(true);
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBSignPop);

        obList.Clear();
    }

    private void Entry()
    {
        if (GameManager.Instance.Triggers.Game_NumGameCompleted)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject gb = Instantiate(ob1, transform.GetChild(i));
                gb.transform.localPosition = new Vector3(0, 0, 0);
                obList.Add(gb);
            }
        }
    }

}
