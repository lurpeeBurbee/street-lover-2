using UnityEngine;

public class BasicFunctions : MonoBehaviour
{

    [SerializeField] int counter;
    [SerializeField] float position;
    [SerializeField] string playerName;
    [SerializeField] bool playerIsAwake;
    [SerializeField] bool canGoLeft;

    [SerializeField] float playerXposition;
    [SerializeField] float playerMoveSpeed;
    SpriteRenderer spriteRenderer;

    void MovePlayer()
    {
        transform.position = new Vector2(playerXposition, transform.position.y);

        if (playerXposition <= 7 && !canGoLeft) // Going right
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.HSVToRGB(playerXposition, 20, 30);

            playerXposition += playerMoveSpeed;
            if (playerXposition >= 7) canGoLeft = true;
        }
        else // Going left
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.red;
            playerXposition -= playerMoveSpeed;
            if (playerXposition <= -6) canGoLeft = false;
        }
    }

    void Start()
    {
        canGoLeft = false;
    }
    void Update()
    {
        MovePlayer();


    }
}
