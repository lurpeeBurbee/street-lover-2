using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // This string stores the name of the scene to be loaded.
    public string sceneName;

    // This method is called when a trigger event occurs.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the triggering object has the tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // Load the specified scene.
            SceneManager.LoadScene(sceneName);
        }
    }
}
