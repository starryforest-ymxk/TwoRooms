using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayer_In_part1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PAMazeInPart1);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NetMsgMgr.Instance.SendMsg(MsgID.Event_0, EventDic.Game_PAMazeInPart1);
        }
    }
}
