using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform castPoint;

    Rigidbody2D rb2d;
    [SerializeField] float speed = 1f;
    [SerializeField] float agroRange;

    bool isFacingLeft;
    private bool isAgro = false;
    private bool isSearching;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (CanSeePlayer(agroRange)) {
            isAgro = true;
        } else {
            if (isAgro) {
                
                if (!isSearching) {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 3);
                }
            }
        }
        if (isAgro) {
            ChasePlayer();
        }
    }
    
    bool CanSeePlayer(float distance) {
        bool val = false;
        float castDist = distance;

        if (isFacingLeft) {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        // hit is looking to make contact with anything on layer Action
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                Debug.Log("see plaayer");
                val = true;
            } else {
                val = false;
            }
            Debug.Log("Hello World");
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        } else {
            Debug.Log("Hello World!!!");
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        return val;


    }


    void ChasePlayer() {
        if (transform.position.x < player.position.x) {
            // enemy is on the left side of the player, so move right
            rb2d.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        } else {
            // enemy is on right side, move left
            rb2d.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer() {
        isSearching = false;
        isAgro = false;
        rb2d.velocity = new Vector2(0, 0);
    }
}
