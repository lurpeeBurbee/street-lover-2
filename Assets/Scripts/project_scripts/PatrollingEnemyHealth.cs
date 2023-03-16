using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyHealth : MonoBehaviour
{
    [SerializeField] Renderer materialRenderer;

    private void OnTriggerEnter2D(Collider2D meleeWeaponCollision)
    {
        if(meleeWeaponCollision.CompareTag("MeleeWeapon"))
        {
            Debug.Log("Enemy hit");
            materialRenderer.material.color = Color.red;
        }
    }


}
