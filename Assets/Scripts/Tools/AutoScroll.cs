using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoScroll : MonoBehaviour
{
    RectTransform rectTransform;
    public float speed;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        rectTransform.position = new Vector2(rectTransform.position.x, rectTransform.position.y + Time.fixedDeltaTime * speed);
    }

    public void Back()
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.OnClickUI);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Menu_Acknowledgements").buildIndex);
    }
}
