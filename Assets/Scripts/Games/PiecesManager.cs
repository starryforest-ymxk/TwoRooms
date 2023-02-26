using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
    private puzzleGame puzzleGame;
    public List<GameObject> piecesA = new List<GameObject>();
    public List<GameObject> piecesB1 = new List<GameObject>();
    public List<GameObject> piecesB2 = new List<GameObject>();
    public GameObject fullPictureA;
    public GameObject fullPictureB;
    private bool holdPiecesA = false;
    private bool holdPiecesB1 = false;
    private bool holdPiecesB2 = false;


    protected void Awake()
    {
        puzzleGame = GameObject.Find("puzzleGame").GetComponent<puzzleGame>();
        EventMgr.GetInstance().AddEventListener("∆¥Õº”Œœ∑ÕÍ≥…", Win);
    }

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener("∆¥Õº”Œœ∑ÕÍ≥…", Win);
    }
    private void Start()
    {
        if(GameManager.Instance.MyPlayer == Player.p1)
        {
            if(GameManager.Instance.Triggers.Game_PAPutPuzzlePieces && !GameManager.Instance.Triggers.Game_PuzzleGameCompleted)
            {
                foreach (var a in piecesA)
                {
                    a.SetActive(true);
                }
            }
            else if(GameManager.Instance.Triggers.Game_PuzzleGameCompleted)
            {
                fullPictureA.SetActive(true);
                fullPictureB.SetActive(true);
            }

        }
        else if (GameManager.Instance.MyPlayer == Player.p2)
        {
            if (!GameManager.Instance.Triggers.Game_PuzzleGameCompleted)
            {
                if (GameManager.Instance.Triggers.Game_PBPutPuzzlePieces1)
                {
                    foreach (var a in piecesB1)
                    {
                        a.SetActive(true);
                    }
                }

                if (GameManager.Instance.Triggers.Game_PBPutPuzzlePieces2)
                {
                    foreach (var a in piecesB2)
                    {
                        a.SetActive(true);
                    }
                }
            }
            else
            {
                fullPictureA.SetActive(true);
                fullPictureB.SetActive(true);
            }
        }
        
    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance.IsHolding(ItemName.∆¥ÕºÀÈ∆¨A))
            {
                holdPiecesA = true;
            }
            else if (GameManager.Instance.IsHolding(ItemName.∆¥ÕºÀÈ∆¨B1))
            {
                holdPiecesB1 = true;
            }
            else if (GameManager.Instance.IsHolding(ItemName.∆¥ÕºÀÈ∆¨B2))
            {
                holdPiecesB2 = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (holdPiecesA)
            {
                EventMgr.GetInstance().InvokeEvent("ÕÊº“A∑≈÷√∆¥ÕºÀÈ∆¨");
                foreach (var a in piecesA)
                {
                    a.SetActive(true);
                }
                BagMgr.Instance.DeleteItem(ItemName.∆¥ÕºÀÈ∆¨A);
                holdPiecesA = false;
            }
            else if (holdPiecesB1)
            {
                EventMgr.GetInstance().InvokeEvent("ÕÊº“B∑≈÷√∆¥ÕºÀÈ∆¨1");
                foreach (var a in piecesB1)
                {
                    a.SetActive(true);
                }
                BagMgr.Instance.DeleteItem(ItemName.∆¥ÕºÀÈ∆¨B1);
                holdPiecesB1 = false;
            }
            else if (holdPiecesB2)
            {
                EventMgr.GetInstance().InvokeEvent("ÕÊº“B∑≈÷√∆¥ÕºÀÈ∆¨2");
                foreach (var a in piecesB2)
                {
                    a.SetActive(true);
                }
                BagMgr.Instance.DeleteItem(ItemName.∆¥ÕºÀÈ∆¨B2);
                holdPiecesB2 = false;
            }
        }

    }

    private void Win()
    {
        //“Ù–ß
        if(GameManager.Instance.MyPlayer == Player.p1)
        {
            fullPictureA.SetActive(true);
            fullPictureB.SetActive(true);
            fullPictureB.GetComponent<Animator>().SetTrigger("other");
        }
        else if(GameManager.Instance.MyPlayer == Player.p2)
        {
            fullPictureA.SetActive(true);
            fullPictureB.SetActive(true);
            fullPictureA.GetComponent<Animator>().SetTrigger("other");

        }
    }



}
