using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4_Controller : MonoBehaviour
{
    public List<GameObject> Additions1 = new List<GameObject>();
    public List<GameObject> Additions2 = new List<GameObject>();
    public List<GameObject> Deletions1 = new List<GameObject>();
    public List<GameObject> Deletions2 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.Triggers.Game_Wheel_IsDone)
        {
            foreach (var a in Additions1)
            {
                a.SetActive(true);
            }
            foreach (var a in Deletions1)
            {
                a.SetActive(false);
            }
        }
        if(GameManager.Instance.Triggers.Game_SignGame)
        {
            foreach (var a in Additions2)
            {
                a.SetActive(true);
            }
            foreach (var a in Deletions2)
            {
                a.SetActive(false);
            }
        }
    }
}
