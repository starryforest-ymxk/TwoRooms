using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RankMgr : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> textProList = new List<TMP_Text>(15);
    private bool hasRead = false;

    private void Update()
    {
        if (!hasRead && NetMsgMgr.Instance.hasReadRank)
        {
            ReadRank();
            hasRead = true;
        }
    }

    private void ReadRank()
    {
        for(int i = 0; i < 5; i++)
        {
            textProList[0 + i * 3].text = NetMsgMgr.Instance.rank[i].nameA;
            textProList[1 + i * 3].text = NetMsgMgr.Instance.rank[i].nameB;
            textProList[2 + i * 3].text = SecToMin(NetMsgMgr.Instance.rank[i].time);
        }
    
    }

    private string SecToMin(int sec)
    {
        int minute = sec / 60;
        int second = sec % 60;
        if (minute == 0)
            return $"{second:D2}s";
        else
            return $"{minute}min {second:D2}s";
    }

    public void Back()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Rank").buildIndex);
    }
}
