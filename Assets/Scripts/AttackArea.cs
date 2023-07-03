using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Enemy")) {
            collider.GetComponent<EnemyHealth>().Damage(damage);
            
        }
    }
}
