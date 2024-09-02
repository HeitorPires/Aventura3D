using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

    public float timeTowait = .2f;

    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void DelayBeforeScene(int scene)
    {
        StartCoroutine(DelayBeforeSceneCoroutine());
        Load(scene);
    }

    IEnumerator DelayBeforeSceneCoroutine()
    {
        yield return new WaitForSeconds(timeTowait);
    }

}
