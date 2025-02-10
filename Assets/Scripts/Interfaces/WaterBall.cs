using UnityEngine;

public class WaterBall : MonoBehaviour
{

    [SerializeField]
    Transform StartPosition;
    [SerializeField]
    GameObject WaterBlastBall;
    [SerializeField]
    float throwForce;
    [SerializeField]
    PlayerMovement playermovement;
    bool isFacingRight;
    [SerializeField]
    float lifeTime;


    float Waterblastdirection()
    {
        isFacingRight = playermovement.GetComponent<PlayerMovement>().isFacingRight;
        float numberi;
        if (!isFacingRight)
        {
            numberi = -1;
        }
        else
        {
            numberi = 1;
        }
        return numberi;
    }
    void WaterBlast()
    {
        GameObject shotWaterBlast = Instantiate(WaterBlastBall, StartPosition.position, Quaternion.identity);
        shotWaterBlast.SetActive(true);
        shotWaterBlast.GetComponent<Rigidbody2D>().AddForce(transform.right * Waterblastdirection() * throwForce);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            WaterBlast();
        }

    }
}


