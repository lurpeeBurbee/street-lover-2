using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    Transform FireStartPosition;
    [SerializeField]
    GameObject Fireblastball;
    [SerializeField]
    float throwForce;

    void Start()
    {
        //Fireblastball = gameObject.GetComponent<GameObject>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(collision.gameObject);
    }

    void FireBlast()
    {

        //Fireblastball.transform.position = FireStartPosition.position;

       GameObject firedBall = Instantiate(Fireblastball, FireStartPosition.position, Quaternion.identity);

        firedBall.SetActive(true);
        firedBall.GetComponent<Rigidbody2D>().AddForce(transform.right * throwForce);

      //  firedBallRB.velocity += new Vector2(1, 0);   
      //  Destroy(Fireblastball, 2f);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            FireBlast();
        }
        //Debug.Log("Time.time: " + Time.time);
        //Debug.Log("Time.deltaTime: " + Time.deltaTime);
    }
}
