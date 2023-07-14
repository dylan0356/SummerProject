using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private Animator animator;
    private float moveSpeed = 4f;
    private float engageDistance = 10f;

    // private float duration = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            // StartCoroutine(NoBox());
            if (PlayerHealth.health <= 0) {
                collision.gameObject.GetComponent<PlayerHealth>().Death();
            }
        }
    }

    // IEnumerator NoBox() {
    //     float elapsed = 0f;
    //     BoxCollider2D bc = GetComponent<BoxCollider2D>();
    //     while (elapsed < duration) {
    //         elapsed += Time.deltaTime;
    //         bc.enabled = false;
    //     }
    //     bc.enabled = true;
    //     return null;
    // }

}
