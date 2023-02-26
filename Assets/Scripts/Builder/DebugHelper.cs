using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelper : MonoBehaviour
{    
    void Start()
    {
        if (GameManager.Instance.Debugmode2)
        {
            BagMgr.Instance.AddItem(ItemName.红色心脏);
            BagMgr.Instance.AddItem(ItemName.绿色心脏);
            BagMgr.Instance.AddItem(ItemName.蓝色心脏);
            BagMgr.Instance.AddItem(ItemName.黄色心脏);
            BagMgr.Instance.AddItem(ItemName.一片羽毛);
        }
        if (GameManager.Instance.Debugmode3)
        {
            BagMgr.Instance.AddItem(ItemName.拼图碎片A);
            BagMgr.Instance.AddItem(ItemName.拼图碎片B1);
            BagMgr.Instance.AddItem(ItemName.拼图碎片B2);
        }
        if (GameManager.Instance.Debugmode4)
        {
            BagMgr.Instance.AddItem(ItemName.神像1);
            BagMgr.Instance.AddItem(ItemName.神像2);
            BagMgr.Instance.AddItem(ItemName.神像3);
        }
    }
}
