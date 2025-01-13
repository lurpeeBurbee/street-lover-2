using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // This string stores the name of the scene to be loaded.
    public string sceneName;

    // This method is called to load the scene.
    public void LoadScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
