using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject UIcanvas;

    void CloseUI()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIcanvas != null)
            {
                if (!UIcanvas.activeSelf)
                {
                    UIcanvas.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    UIcanvas.SetActive(false);
                    Time.timeScale = 1; 
                }
            }
        }
    }

}
