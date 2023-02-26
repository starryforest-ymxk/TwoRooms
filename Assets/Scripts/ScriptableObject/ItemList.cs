using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemList", menuName = "Custom/ItemList")]
public class ItemList : ScriptableObject
{
    public List<ItemDetail> list;
    public ItemDetail GetItem(ItemName itemName) => list.Find(t => t.name.Equals(itemName));    
}

