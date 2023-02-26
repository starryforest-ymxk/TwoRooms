using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  所有互动物品的基类
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class Interactive : MonoBehaviour
{
    [SerializeField] protected ItemName[] requiredItem;
    [SerializeField] protected bool isDone;
    protected abstract bool IsAvailiable { get; }
    protected virtual void Awake()
    {
        this.gameObject.tag = "Interactive";
    }
    public virtual void Interact()
    {
        if (!IsAvailiable) return;
    }
    public bool HasRequiredItem()
    {
        foreach (var item in requiredItem)
        {
            if (!GameManager.Instance.ItemList.Contains(item)) return false;
        }
        return true;
    }
}
