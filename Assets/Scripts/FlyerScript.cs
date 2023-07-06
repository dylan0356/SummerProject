using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MonoBehaviour
{
    [SerializeField] private Transform player;


    private float moveSpeed;
    private float engageDistance = 10f;


    
    void Start()
    {
        moveSpeed = Random.Range(3f, 6f);
    }

    
    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= engageDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player hit");
            PlayerHealth.health--;
            if (PlayerHealth.health <= 0) {
                collision.gameObject.GetComponent<PlayerHealth>().Death();
            }
            
            // collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        //update for player attack eventually
    }

}
