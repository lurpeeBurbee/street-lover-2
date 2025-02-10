using System.Collections.Generic;
using UnityEngine;

public class HealthBoxes : MonoBehaviour
{
    [SerializeField]
    List<GameObject> healthBoxes;

    int DamagePoint = 1;
    int HealthBoxListIndex = 0;

    void ReduceHealthBox()
    {
        healthBoxes[HealthBoxListIndex].SetActive(false);

        if (HealthBoxListIndex < healthBoxes.Count-1)
        {
            HealthBoxListIndex += DamagePoint;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) {
            ReduceHealthBox();
        }
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
