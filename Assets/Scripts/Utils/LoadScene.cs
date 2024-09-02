using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }
}
