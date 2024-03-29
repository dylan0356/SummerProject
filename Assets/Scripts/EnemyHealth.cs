using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // was public before but breaking so trying to makke it private and seeing ifthat works
    private int health = 1;
    public GameObject heart;

    

    void Start() {
        heart = GameObject.Find("Heart");
    }

    public void Damage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            // Randomly drop a heart
            int random = Random.Range(0, 5);
            if (random == 1) {
                Instantiate(heart, transform.position, Quaternion.identity);
            }
            ScoreScript.AddScore(10);
        }
    }
}
