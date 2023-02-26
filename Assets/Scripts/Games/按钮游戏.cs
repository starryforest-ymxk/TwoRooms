using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

public class 按钮游戏 : MonoBehaviour
{

    public bool canClick = true;
    private bool otherCompleted = false;
    private bool myCompleted = false;
    private enum GameStage {start, player1, player2, both, success}
    private enum Click { my, other, none}

    private List<Click> click = new List<Click>();

    private GameStage gameStage = GameStage.start;

    private GameObject indicator0;
    private GameObject indicator1;

    private bool ready = false;
    private bool otherReady = false;

    [SerializeField] private float offset;
    [SerializeField] private float duration;

    private bool MyPlayerFirst = false;
    private bool success = false;


    private void Awake()
    {
        Entry();
        EventMgr.GetInstance().AddEventListener(EventDic.Game_OtherPlayerClickButton, OtherPlayerClick);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_ButtonGameOtherCompleted, OtherComplete);
    }

    private void OnDestroy()
    {
        gameStage = GameStage.start;
        click.Clear();
        indicator0.SetActive(true);
        indicator1.SetActive(true);
        ready = false;
        otherReady = false;
        otherCompleted = false;
        myCompleted = false;
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_OtherPlayerClickButton, OtherPlayerClick);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_ButtonGameOtherCompleted, OtherComplete);
    }

    private void Start()
    {
        indicator0 = gameObject.transform.GetChild(0).gameObject;
        indicator1 = gameObject.transform.GetChild(2).gameObject;
    }


    public void MyPlayerClick()
    {
        if (click.Count > 2 || !canClick) return;  
        NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_OtherPlayerClickButton);

        if (gameStage == GameStage.start)
        {
            ready = true;
            GameStart();
        }

        else
        {
            click.Add(Click.my);

            CheckResult();

            Invoke("Wait", 2f);
        }
    }

    private void OtherPlayerClick()
    {
        if (click.Count > 2 || !canClick) return;
        Debug.Log(gameStage);
        Debug.Log(ready);
        Debug.Log(otherReady);

        if (gameStage == GameStage.start)
        {
            otherReady = true;
            GameStart();
        }

        else
        {
            click.Add(Click.other);

            CheckResult();

            Invoke("Wait", 2f);
        }
    }

    private void Wait()
    {
        if(click.Count == 1)
        {
            click.Add(Click.none);
            CheckResult();
        }
    }

    private void CheckResult()
    {
        if (click.Count != 2) return;
        canClick = false;
        switch (gameStage)
        {
            case GameStage.player1:
                if(click.Contains(Click.my)&&!click.Contains(Click.other))
                {
                    MyPlayerFirst = true;
                    indicator0.SetActive(false);
                    click.Clear();
                    Invoke("Stage2", 1.5f);

                }
                else if(click.Contains(Click.other)&&!click.Contains(Click.my))
                {
                    MyPlayerFirst = false;
                    indicator0.SetActive(false);
                    click.Clear();
                    Invoke("Stage2", 1.5f);
                }
                else
                {
                    indicator0.SetActive(false);
                    Invoke("GameReset", 1f);
                }
                break;
            case GameStage.player2:
                if(MyPlayerFirst && click.Contains(Click.other) && !click.Contains(Click.my))
                {
                    indicator1.SetActive(false);
                    click.Clear();
                    Invoke("Stage3", 2 * Settings.NetDelayTime);
                }
                else if(!MyPlayerFirst && click.Contains(Click.my) && !click.Contains(Click.other))
                {
                    indicator1.SetActive(false);
                    click.Clear();
                    Invoke("Stage3", 2 * Settings.NetDelayTime);
                }
                else
                {
                    indicator1.SetActive(false);
                    Invoke("GameReset", 1f);
                }
                break;
            case GameStage.both:
                if (click.Contains(Click.other) && click.Contains(Click.my))
                {
                    indicator0.SetActive(false);
                    indicator1.SetActive(false);
                    NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_ButtonGameOtherCompleted);
                    myCompleted = true;
                    Invoke("Success", 2 * Settings.NetDelayTime); 
                }
                else
                {
                    indicator0.SetActive(false);
                    indicator1.SetActive(false);
                    Invoke("GameReset", 1f);
                }
                break;
            default:
                break;
        }

        click.Clear();

    }


    private void GameStart()
    {
        if (!ready || !otherReady) return;
        indicator0.SetActive(false);
        indicator1.SetActive(false);
        Invoke("Stage1", 2f);
    }

    private void GameReset()
    {
        if (myCompleted && otherCompleted)
            return;
        myCompleted = false;
        otherCompleted = false;
        gameStage = GameStage.start;
        click.Clear();
        indicator0.SetActive(true);
        indicator1.SetActive(true);
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_ButtonPop);
        Invoke("GameStart", 0.5f);
    }
    private void Stage1()
    {
        gameStage ++;
        indicator0.SetActive(true);
        canClick=true;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_ButtonPop);
    }

    private void Stage2()
    {
        gameStage ++;
        indicator1.SetActive(true);
        canClick = true;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_ButtonPop);
    }
    private void Stage3()
    {
        gameStage++;
        indicator0.SetActive(true);
        indicator1.SetActive(true);
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_ButtonPop);
        canClick = true;
    }

    private void OtherComplete()
    {
        otherCompleted = true;
        Invoke("Success", 2 * Settings.NetDelayTime);
    }

    private void Success()
    {
        if (myCompleted && otherCompleted)
            AfterWin();
        else
            GameReset();
    }

    private void AfterWin()
    {
        if (success) return;
        success = true;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_ButtonGameCompleted);
        transform.DOMoveX(offset, duration);

    }

    private void Entry()
    {
        if (GameManager.Instance.Triggers.Game_ButtonGameCompleted)
            transform.localPosition = new Vector3(offset,transform.localPosition.y,transform.localPosition.z);
    }


}
