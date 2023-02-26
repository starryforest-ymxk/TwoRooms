using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
public sealed class Transitioner : MonoBehaviour
{
    private CanvasGroup group;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        group = this.GetComponent<CanvasGroup>();
    }
    public void Trans(string from, string to, float fadeTime)
    {
        StartCoroutine(RealTrans(from, to, fadeTime));
    }
    private IEnumerator RealTrans(string from, string to, float fadeTime)
    {
        EventMgr.GetInstance().InvokeEvent(EventDic.SetCannotClick);
        WaitForSeconds w = new WaitForSeconds(fadeTime);
        DOTween.To(() => group.alpha, x => group.alpha = x, 1, fadeTime);
        yield return w;
        yield return StartCoroutine(RealTP(from, to));
        DOTween.To(() => group.alpha, x => group.alpha = x, 0, fadeTime);
        yield return w;
        EventMgr.GetInstance().InvokeEvent(EventDic.SetCanClick);
        Destroy(this.gameObject);
    }
    private IEnumerator RealTP(string from, string to)
    {
        if (!from.Equals(string.Empty)) yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        Scene s = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(s);
    }
}
