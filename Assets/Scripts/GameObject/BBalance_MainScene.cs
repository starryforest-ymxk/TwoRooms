using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBalance_MainScene : BBalance
{
    public override void Interact() { }
    protected override void Awake()
    {
        base.Awake();
        for (int i = 1; i < transform.childCount; i++)
        {
            Collider2D c;
            transform.GetChild(i).TryGetComponent<Collider2D>(out c);
            if (c != null) c.enabled = false;
        }
    }
}
