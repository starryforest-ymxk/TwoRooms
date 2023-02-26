using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : MonoBehaviour
{
    Dictionary<string, List<GameObject>> Pool = new Dictionary<string, List<GameObject>>();
    GameObject PoolObj = null;

    public GameObject GetOutPool(string pathName)
    {
        GameObject obj = null;
        if (Pool.ContainsKey(pathName) && Pool[pathName].Count > 0)
        {
            obj = Pool[pathName][0];
            Pool[pathName].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(pathName));
        }
        obj.name = pathName;
        obj.SetActive(true);
        return obj;
    }
    public void GetInPool(string pathName, GameObject obj)
    {
        if (PoolObj == null)
        { PoolObj = new GameObject(); }
        obj.SetActive(false);
        if (Pool.ContainsKey(pathName))
        {
            Pool[pathName].Add(obj);
        }
        else
        {
            Pool.Add(pathName, new List<GameObject>() { obj });
        }
        obj.transform.parent = PoolObj.transform;
    }
    public void Clear()
    {
        PoolObj = null;
        Pool.Clear();
        
    }


}
