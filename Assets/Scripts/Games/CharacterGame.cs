using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CharacterGame : MonoBehaviour
{
    public Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private int[] slot = { -1, -1, -1, -1 };
    public List<int> answer;

    public List<GameObject> slots;
    public List<GameObject> characters;
    public float zoneHalfWidth;
    public GameObject gate;
    public Sprite open;
    public GameObject idol;

    private void Awake()
    {
        if(GameManager.Instance.Triggers.Game_SignGame)
        {
            gate.GetComponent<SpriteRenderer>().sprite = open;
            idol.SetActive(true);
            foreach (var a in characters)
            {
                if (a.GetComponent<Character>().characterNum == answer[0] || a.GetComponent<Character>().characterNum == answer[1]
                    || a.GetComponent<Character>().characterNum == answer[2] || a.GetComponent<Character>().characterNum == answer[3])
                {
                    a.transform.position = slots[answer.IndexOf(a.GetComponent<Character>().characterNum)].transform.position;
                    a.transform.eulerAngles = Vector3.zero;
                }
            }
        }
        else 
            idol.SetActive(false);
    }
    public void SetCharacter(int num, int characterNum)
    {
        slot[num] = characterNum;
        CheckWin();
    }
    public void PutBack(int num)
    {
        slot[num] = -1;
    }
    public void CheckWin()
    {
        bool win = true;
        for(int i = 0; i<4;i++)
        {
            if (slot[i] != answer[i])
                win = false;
        }
        if(win)
        {
            Invoke("Win", 0.5f);
        }
    }
    public void Win()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.Game_SignGame);
        idol.SetActive(true);
        gate.GetComponent<SpriteRenderer>().sprite = open;
    }

}