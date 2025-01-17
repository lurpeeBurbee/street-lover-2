using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinja : MonoBehaviour
{
    public float speed = 2f;
    public float growAmount = 0.1f;
    private Vector3 originalScale;
    private bool hit = false;
    private bool movingRight = true;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!hit)
        {
            float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float leftBound = -screenWidth;
            float rightBound = screenWidth;

            if (movingRight)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.right);
                if (transform.position.x > rightBound)
                {
                    movingRight = false;
                }
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector2.left);
                if (transform.position.x < leftBound)
                {
                    movingRight = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(GrowAndDisappear());
        }
    }

    private IEnumerator GrowAndDisappear()
    {
        hit = true;
        Debug.Log("Enemy hit!");

        Vector3 targetScale = originalScale + new Vector3(growAmount, growAmount, growAmount);

        while (transform.localScale.x < targetScale.x)
        {
            transform.localScale += new Vector3(growAmount, growAmount, growAmount) * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
