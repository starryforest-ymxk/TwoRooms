using UnityEngine;

public class puzzleSlot : MonoBehaviour
{
    public int num;
    private bool isOccupied = false;
    private puzzleGame manager;
    private GameObject currentPiece;
    private GameObject pieces;

    private void Start()
    {
        manager = GameObject.Find("puzzleGame").GetComponent<puzzleGame>();
        pieces = GameObject.Find("pieces");
        Debug.Log(pieces == null);
    }

    public void SetPuzzle(GameObject piece)
    {
        if (isOccupied) return;
        isOccupied = true;
        piece.GetComponent<puzzlePiece>().Move(transform.position);
        piece.transform.SetParent(this.transform);
        manager.SetPiece(num, piece.GetComponent<puzzlePiece>().pieceNum);
        piece.GetComponent<puzzlePiece>().placed = true;
        currentPiece = piece;
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_PutPuzzlePieces);
    }
    public void PickUp()
    {
        if (!isOccupied) return;
        isOccupied = false;
        currentPiece.transform.SetParent(pieces.transform);
        manager.PutBack(num);
        currentPiece.GetComponent<puzzlePiece>().placed = false;
        currentPiece = null;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOccupied
            && manager.mouseWorldPos.x < transform.position.x + manager.zoneHalfWidth && manager.mouseWorldPos.x > transform.position.x - manager.zoneHalfWidth
                && manager.mouseWorldPos.y < transform.position.y + manager.zoneHalfWidth && manager.mouseWorldPos.y > transform.position.y - manager.zoneHalfWidth)
        {
            PickUp();
        }
    }
}
