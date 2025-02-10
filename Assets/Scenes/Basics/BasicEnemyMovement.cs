using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] float enemyMoveValue;
    [SerializeField] float moveSpeed;
    readonly float enemyMaxLeftPosition = -7.3f;
    public bool canMoveLeft;
    public SpriteRenderer spriteRenderer;   
    void MoveEnemy()
    {
        if (transform.position.x > enemyMaxLeftPosition && canMoveLeft == true) // toimii myös pelkkä canMoveLeft
            // jos pitää checkata että on false, niin kirjoita !canMoveLeft
        {
            transform.position = new Vector2(enemyMoveValue -= moveSpeed * Time.deltaTime, transform.position.y);
            if(transform.position.x <= -7.3) canMoveLeft = false;   

        }
        else
        {
            transform.position = new Vector2(enemyMoveValue += moveSpeed * Time.deltaTime, transform.position.y);
            if (transform.position.x >= 7.3) canMoveLeft = true;
        }
        
    }
    void Start()
    {
        spriteRenderer.color = Color.white; 
        canMoveLeft = true;
    }
    void Update()
    {
        MoveEnemy();
    }


}
