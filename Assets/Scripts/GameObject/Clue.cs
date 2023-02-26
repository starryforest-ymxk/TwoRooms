using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{
    private static bool isShowing = false;
    private CanvasGroup group;
    public Image clueImage;
    public Sprite mapSprite;
    public Sprite colorClueSprite;
    public Sprite signClueSprite;


    private void OnEnable()
    {
        group = GetComponent<CanvasGroup>();
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBShowMap, ShowMap);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PAShowColorClue, ShowColorClue);
        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBShowSignClue, ShowSignClue);
    }
    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBShowMap, ShowMap);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PAShowColorClue, ShowColorClue);
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBShowSignClue, ShowSignClue);
    }
    public void ShowMap()
    {
        isShowing = true;
        clueImage.sprite = mapSprite;
        group.SetGroupOn();
    }

    public void ShowSignClue()
    {
        isShowing = true;
        clueImage.sprite = signClueSprite;
        group.SetGroupOn();
    }

    public void ShowColorClue()
    {
        isShowing = true;
        clueImage.sprite = colorClueSprite;
        group.SetGroupOn();
    }
    public void CloseClue()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_CloseClue);
        isShowing = false;
        group.SetGroupOff();
    }
}
