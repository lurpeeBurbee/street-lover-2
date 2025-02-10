using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    Animator meleeWeaponAnimator;
    public bool meleeAttack;

    void Start()
    {
        meleeWeaponAnimator = GetComponent<Animator>(); 
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
          meleeWeaponAnimator.SetBool("meleeAttack", true);
        }
        else
        {
            meleeWeaponAnimator.SetBool("meleeAttack", false);
        }
    }
}
