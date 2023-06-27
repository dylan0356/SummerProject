using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int health = 5;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Death();
        }
    }

    private void Death() {
        Destroy(gameObject);
    }
}
