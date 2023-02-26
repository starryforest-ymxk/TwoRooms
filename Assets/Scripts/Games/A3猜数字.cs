using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

public class A3猜数字 : MonoBehaviour
{
   
    [SerializeField] private float offset = 7.16f;
    [SerializeField] private float duration = 2.5f;
    [Tooltip("尝试次数")]
    [SerializeField] private int attemptTimes = 8;
    private int[] result = new int[4];
    public List<A3猜数字符号> signList = new List<A3猜数字符号>(4);
    public bool reset = false;
    public int correctNum;
    public int wrongPlace;
    private int[] temp = new int[4];
    private int wrongtime = 0;

    public GameObject feather;
    public GameObject colorTip;


    private void Start()
    {
        
        Entry();
        GameReset();
    }


    public void RemoveSign()
    {
        if(signList.Count == 4)
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PANotPut4Sign);
        
        if(signList.Count > 0)
        {
            signList.Remove(signList[signList.Count-1]);
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPickSign);
        }
       
    }

    public void AddSign(A3猜数字符号 sign)
    {

        if (signList.Count<=3)
        {
            signList.Add(sign);
            sign.index = signList.Count-1;
            EventMgr.GetInstance().InvokeEvent(EventDic.Game_PAPutSign);
        }
        if (signList.Count == 4)
        {
            GetTips();
        }
    }

    private void GetTips()
    {
        correctNum = 0;
        wrongPlace = 0;


        if (signList.Count != 4)
            return;

        int[] a = new int[4];

        for(int i = 0; i< 4; i++)
        {
            a[i] = signList[i].Num;
            temp[i] = result[i];
        }

        for (int i = 0; i < 4; i++)
        {
            if(temp[i] == a[i])
            {
                correctNum++;
                a[i] = -1;
                temp[i] = -2;
            }
        }
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(a[i] == temp[j])
                {
                    a[i] = -1;
                    temp[j] = -2;
                    wrongPlace++;
                    break;
                }
            }
        }
        Debug.Log(correctNum);
        Debug.Log(wrongPlace);

        if (correctNum != 4)
            wrongtime++;

        if (wrongtime <= attemptTimes-1)
        {
            NetMsgMgr.Instance.SendMsg<string>(EventDic.Game_PAPut4Sign, correctNum.ToString() + "~" + wrongPlace.ToString());
            if (correctNum == 4) Win();
        }
        else
        {
            foreach (var sign in signList)
                Destroy(sign.gameObject);
            GameReset();
            reset = true;
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PAReset);
            wrongtime = 0;
            Invoke("WaitForReset", 3f);
        }

    }


    private void GameReset()
    {
        signList.Clear();
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = Random.Range(0, 6);
            temp[i] = result[i];
        }
    }

    private void WaitForReset()
    {
        reset = false;
        Debug.Log(reset);
    }



    private void Win()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_NumGameCompleted);
        Invoke("AfterWin", 2f);
    }

    private void AfterWin()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_NumGameCompletedStoneMove);
        transform.parent.gameObject.transform.DOMoveX(offset, duration);
        feather.SetActive(true);
        colorTip.SetActive(true);
    }

    private void Entry()
    {
        if(GameManager.Instance.Triggers.Game_NumGameCompleted)
        {
            transform.parent.gameObject.transform.localPosition = new Vector3(offset, transform.parent.gameObject.transform.localPosition.y, transform.parent.gameObject.transform.localPosition.z);
            feather.SetActive(true);
            colorTip.SetActive(true);
        }
        else
        {
            feather.SetActive(false);
            colorTip.SetActive(false);
        }
        
    }


}
