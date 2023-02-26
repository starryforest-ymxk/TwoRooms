using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_MainConcoller : MonoBehaviour
{
    public List<GameObject> Addition = new List<GameObject>();
    public List<GameObject> Deletion = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.Triggers.Game_Coffin_IsDone)
        {
            for (int i = 0; i < Addition.Count; i++)
            {
                Addition[i].SetActive(true);
            }
            for (int i = 0; i < Deletion.Count; i++)
            {
                Deletion[i].SetActive(false);
            }
        }
    }
}
