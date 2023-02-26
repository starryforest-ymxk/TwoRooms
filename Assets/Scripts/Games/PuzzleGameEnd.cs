using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameEnd : MonoBehaviour
{
    public void Back()
    {
        if (GameManager.Instance.MyPlayer == Player.p1)
            SceneMgr.Instance.TP("A2_拼图", "A2");
        else if (GameManager.Instance.MyPlayer == Player.p2)
            SceneMgr.Instance.TP("B2_拼图", "B2");
    }
}
