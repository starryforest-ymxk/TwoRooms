using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SetFire : MonoBehaviour
{
    [Header("亮度设置")]
    [Space]
    [Tooltip("点亮祭台后有火焰场景的全局光亮度")]
    [SerializeField] private float lightIntensityWithFire = 0.3f;
    [Tooltip("点亮祭台后无火焰场景的全局光亮度")]
    [SerializeField] private float lightIntensityWithoutFire = 0.6f;
    [Tooltip("点亮祭台前的全局光亮度")]
    [SerializeField] private float lightIntensityBeforeFire = 0.1f;

    private void OnDisable()
    {
        EventMgr.GetInstance().DeleteEventListener(EventDic.Game_PBPushSun, Fire);
    }
    private void Awake()
    {

        EventMgr.GetInstance().AddEventListener(EventDic.Game_PBPushSun, Fire);

        if (GameManager.Instance.Debugmode1)
        {
            GameManager.Instance.Triggers.Game_PBPushSun = true;
        }

        if (gameObject.transform.childCount == 1)
        {
            if (GameManager.Instance.Triggers.Game_PBPushSun)
                gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityWithoutFire;
            else
                gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityBeforeFire;
        }

        else if (gameObject.transform.childCount == 2)
        {
            if (GameManager.Instance.Triggers.Game_PBPushSun)
            {
                gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityWithFire;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityBeforeFire;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
    private void Fire()
    {
        if (gameObject.transform.childCount == 1)
        {
            gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityWithoutFire;
        }
        else if (gameObject.transform.childCount == 2)
        {
            gameObject.transform.GetChild(0).GetComponent<Light2D>().intensity = lightIntensityWithFire;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
