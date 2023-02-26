using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Reloader : MonoBehaviour
{
    [SerializeField] private GameObject[] objToCreate;
    private GameObject[] objToDestroy;
    private bool reloadWithConnection = false;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        objToDestroy = new GameObject[3];
        objToDestroy[0] = GameObject.Find("GameManager");
        objToDestroy[1] = GameObject.Find("Net");
        objToDestroy[2] = GameObject.Find("PhotonMono");
    }
    private void Start()
    {
        if (reloadWithConnection) ReloadWithConnection();
        else Reload();
    }
    public void SetType(bool _reloadWithConnection)
    {
        reloadWithConnection = _reloadWithConnection;
    }
    private void Reload()
    {
        StartCoroutine(reload());
        IEnumerator reload()
        {
            var s = SceneManager.GetActiveScene();
            yield return SceneManager.UnloadSceneAsync(s);
            for (int i = 0; i < objToDestroy.Length; i++)
            {
                var o = objToDestroy[i];
                Destroy(o);
            }
            //objToCreate[0].GetComponent<GameManager>().ReInstance();
            //GameObject.Instantiate(objToCreate[0]);
            //objToCreate[1].GetComponent<Net>().ReInstance();
            //GameObject.Instantiate(objToCreate[1]);
            s = SceneManager.GetActiveScene();
            yield return SceneManager.LoadSceneAsync("permanent");            

            //SceneMgr.Instance.TP("permanent", "permanent");
            //Destroy(this.gameObject);
        }
    }
    private void ReloadWithConnection()
    {
        StartCoroutine(reload());
        IEnumerator reload()
        {
            var s = SceneManager.GetActiveScene();
            yield return SceneManager.UnloadSceneAsync(s);
            var o = objToDestroy[0];
            Destroy(o);
            //objToCreate[0].GetComponent<GameManager>().ReInstance();
            //GameObject.Instantiate(objToCreate[0]);
            //Debug.Log("Create:" + objToCreate[0]);
            //yield return SceneManager.LoadSceneAsync("permanent");
            SceneMgr.Instance.TP("permanent", "permanent");
            Destroy(this.gameObject);
        }
    }
}
