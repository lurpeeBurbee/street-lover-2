using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallLife : MonoBehaviour
{
    [SerializeField]
    float lifeTime;
    [SerializeField]
    float ballSize;
    [SerializeField]
    GameObject SplashEffect;

    private void Start()
    {
        SplashEffect.GetComponent<GameObject>();
    }
    private void WaterBallLifeTime()
    {
        lifeTime -= 1f * Time.deltaTime;
        transform.localScale = new Vector3(lifeTime * 0.2f, lifeTime * 0.2f, 1);

        if(lifeTime < 2.7f) SplashEffect.transform.SetParent(null); // Irtaudu Parentista

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            Destroy(SplashEffect);
        }
    }
    void Update()
    {
        WaterBallLifeTime();
    }
}
