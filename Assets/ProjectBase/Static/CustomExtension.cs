using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public static class CustomExtension
{
    public static void SetGroupOn(this CanvasGroup group)
    {
        group.DOKill();
        DOTween.To(() => group.alpha, x => group.alpha = x, 1, Settings.HubFadeTime);
        group.interactable = true;
        group.blocksRaycasts = true;
    }
    public static void SetGroupOff(this CanvasGroup group)
    {
        group.DOKill();
        DOTween.To(() => group.alpha, x => group.alpha = x, 0, Settings.HubFadeTime);
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
