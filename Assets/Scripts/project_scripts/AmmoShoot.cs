using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoShoot : MonoBehaviour
{
    [SerializeField] Transform AmmoStartPosition;
    [SerializeField] GameObject AmmoPrefab;

    void ShootAmmo()
    {
        GameObject ammoObject = Instantiate(AmmoPrefab, AmmoStartPosition.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.RightControl) )
        {
            ShootAmmo();
        }
    }
}
