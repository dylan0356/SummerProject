using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MonoBehaviour
{
    private Transform player;
    
    private Animator animator;
    private float moveSpeed = 4f;
    private float engageDistance = 10f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= engageDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth.health--;
            if (PlayerHealth.health <= 0) {
                collision.gameObject.GetComponent<PlayerHealth>().Death();
            }
        }
    }
}
