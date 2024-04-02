using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Shop");
    }
}
