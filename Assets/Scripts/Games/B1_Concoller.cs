using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_Concoller : MonoBehaviour
{
    public List<GameObject> Additions = new List<GameObject>();
    public List<GameObject> Deletions = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.Triggers.Game_Coffin_IsDone)
        {
            for (int i = 0; i < Additions.Count; i++)
            {
                Additions[i].SetActive(true);
            }
            for (int i = 0; i < Deletions.Count; i++)
            {
                Deletions[i].SetActive(false);
            }
        }
    }
}
