using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayer_in_part4 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PAMazeInPart4);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PAMazeInPart4);
        }
    }
}
