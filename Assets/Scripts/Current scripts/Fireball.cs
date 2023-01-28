using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    Transform FireStartPosition;
    [SerializeField]
    GameObject Fireblastball;

    void Start()
    {
        //Fireblastball = gameObject.GetComponent<GameObject>();
    }

    void FireBlast()
    {

        //Fireblastball.transform.position = FireStartPosition.position;

       GameObject firedBall = Instantiate(Fireblastball, FireStartPosition.position, Quaternion.identity);
      Rigidbody2D firedBallRB = Fireblastball.GetComponent<Rigidbody2D>();

        firedBallRB.velocity += new Vector2(1, 0);   
        Destroy(Fireblastball, 2f);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            FireBlast();
        }
    }
}
