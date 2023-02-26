using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Interactive
{
    protected override bool IsAvailiable => true;
    private Color color_down;
    private Color color_up;

    private void Start()
    {
        color_down = new Color(0.83f, 0.06f, 0f, 1f);
        color_up = new Color(0.83f, 0.06f, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            gameObject.GetComponent<SpriteRenderer>().color = color_up;
        }
    }

    public override void Interact()
    {
        gameObject.GetComponent<SpriteRenderer>().color = color_down;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PBPushSunLocal);
        if (!GameManager.Instance.Triggers.Game_PBPushSun)
        {
            EventMgr.GetInstance().InvokeTwoSideEvent(EventDic.Game_PBPushSun);
        }
    }
}


