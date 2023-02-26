using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3猜数字符号 : Interactive
{
    [SerializeField] private int num;

    public A3猜数字 a3;
    public GameObject SignToCreate;
    public int Num => num;

    protected override bool IsAvailiable => GameManager.Instance.Triggers.Game_ButtonGameCompleted && !GameManager.Instance.Triggers.Game_NumGameCompleted && !a3.reset;

    public int index;

    public bool putin;

    public override void Interact()
    {
        Debug.Log(a3.reset);
        Debug.Log(IsAvailiable);
        if (!IsAvailiable)
            return;
        OnClick();
    }



    public void OnClick()
    {
        Debug.Log("click");
        if(putin)
        {
            if(index == a3.signList.Count-1)
            {
                a3.RemoveSign();
                Destroy(gameObject);
            }
        }
        else
        {
            if(a3.signList.Count<=3)
            {
                Debug.Log("instantiate");
                GameObject go = Instantiate(SignToCreate);
                go.transform.SetParent(a3.gameObject.transform.GetChild(a3.signList.Count));
                go.transform.localPosition= new Vector3(0, 0, 0);
                go.GetComponent<A3猜数字符号>().a3 = GameObject.FindGameObjectWithTag("Holder").GetComponent<A3猜数字>();
                SignToCreate.GetComponent<A3猜数字符号>().putin = true;
                a3.AddSign(go.GetComponent<A3猜数字符号>());
            }
        }
    }

}
