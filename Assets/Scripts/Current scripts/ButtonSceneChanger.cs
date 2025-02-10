using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChanger : MonoBehaviour
{

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
