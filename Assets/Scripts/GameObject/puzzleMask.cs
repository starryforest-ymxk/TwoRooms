using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleMask : MonoBehaviour
{
    public GameObject puzzleMaskTeleport;
    private void Start()
    {
        if (!GameManager.Instance.Triggers.Game_Balance_IsDone && !GameManager.Instance.Debugmode3) 
            puzzleMaskTeleport.GetComponent<BoxCollider2D>().enabled = false;
        else 
            gameObject.SetActive(false);
    }
}
