using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageQualityControl : MonoBehaviour
{


    public void SetImageQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);   
        Debug.Log("Quality level is now: " + quality);
    }
}
