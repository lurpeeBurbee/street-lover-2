using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    [SerializeField] float ammoMove;
    [SerializeField] float ammoSpeed;

    IEnumerator AmmoLifeSpan()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        StartCoroutine(AmmoLifeSpan());
    }

    void Update()
    {
        ammoMove += 1;
        transform.position = new Vector2 (transform.position.x + ammoMove * Time.deltaTime, transform.position.y);
    }
}
