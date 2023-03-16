using UnityEngine;

public class PatrollingEnemyHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer materialRenderer;

    float redValue = 0f;
    [SerializeField] float colorChangeSpeed;
    public bool enemyIsHit = false;
    Color takingHitColor = new(0, 0, 0);

    private void OnTriggerEnter2D(Collider2D meleeWeaponCollision)
    {
        if (meleeWeaponCollision.CompareTag("MeleeWeapon"))
        {
            Debug.Log("Enemy hit");
            enemyIsHit = true;
        }
    }


    private void Update()
    {
        takingHitColor = new(redValue, 0, 0);
        materialRenderer.color = takingHitColor;

        if (enemyIsHit && redValue < 1f)
        {
            //Debug.Log("redValue: " + redValue);
            redValue += colorChangeSpeed * Time.deltaTime;
            if (redValue >= 1f)
            {
                enemyIsHit = false;
            }
        }
        if (!enemyIsHit && redValue >= 0f)
        {
            //Debug.Log("redValue: " + redValue);
            redValue -= colorChangeSpeed * Time.deltaTime;
        }
    }


}
