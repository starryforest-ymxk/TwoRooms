using UnityEngine.SceneManagement;
using UnityEngine;

public class permanent : MonoBehaviour
{
    void Start()
    {
        bool debug = GameManager.Instance.SceneDebugMode;
#if UNITY_EDITOR
        if (!debug)
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        return;
#endif
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
