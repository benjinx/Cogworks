using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string scene, LoadSceneMode mode)
    {
        SceneManager.LoadScene(scene, mode);
    }
}
