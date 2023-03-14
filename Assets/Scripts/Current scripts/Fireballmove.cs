using UnityEngine;
public class Fireballmove : MonoBehaviour
{
    public float ammoMove;
    public float speed = 20;

    private void Start()
    {
    
    }
    void Update()
    {

       ammoMove += 1;
        transform.position = new Vector2(transform.position.x + ammoMove * Time.deltaTime, transform.position.y);
        


    }
}