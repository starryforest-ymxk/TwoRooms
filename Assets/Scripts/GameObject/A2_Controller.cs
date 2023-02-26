using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_Controller : MonoBehaviour
{
    public List<GameObject> Additions = new List<GameObject>();
    public List<GameObject> Deletions = new List<GameObject>();
    public List<GameObject> Additions1 = new List<GameObject>();
    private void Awake()
    {
        if(GameManager.Instance.EndDebug)
        {
            GameManager.Instance.Triggers.Game_PuzzleGameCompleted = true;
            GameManager.Instance.Triggers.Game_Balance_IsDone = true;
            GameManager.Instance.Triggers.Game_Maze_IsDone = true;
        }
    }
    void Start()
    {
        if(GameManager.Instance.Triggers.Game_PuzzleGameCompleted)
        {
            foreach(var a in Additions)
            {
                a.SetActive(true);
            }
            foreach(var b in Deletions)
            {
                b.SetActive(false);
            }    
        }
        if (GameManager.Instance.Triggers.Game_Maze_IsDone)
        {
            foreach (var a in Additions1)
            {
                a.SetActive(true);
            }
        }
    }
}
