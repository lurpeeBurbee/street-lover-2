using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField]        
    TextMeshProUGUI mText;
    [SerializeField]    
    string Uitext;
    [SerializeField] GameObject collectable;
    float infoVisibleTime;
    void Start()
    {
        mText.GetComponent<TextMeshProUGUI>();
        infoVisibleTime = 4f;
        
    }


    void Update()
    {
        if(!collectable.activeSelf && infoVisibleTime >= 0) {
            infoVisibleTime -= Time.deltaTime * 3f;
            if(infoVisibleTime > 0) {
    mText.text = Uitext;
            }
            else { mText.text = "";
                return;
            }
            
        }
    }
}
