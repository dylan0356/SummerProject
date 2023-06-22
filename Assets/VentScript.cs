using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    
    private Collider2D ventCollider;

    void Start()
    {
        ventCollider = GetComponent<Collider2D>();
    }

    
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Player has opened vent door");
            ventCollider.isTrigger = true;
        }
    }

    // void OnCollisionStay2D(Collision2D collision) {
    //     if (collision.gameObject.tag == "Player") {
    //         Debug.Log("Player is in vent");
    //         ventCollider.enabled = false;
    //     }
    // }

    void OnCollisionExit2D() {
        // not happpening bellow VVV
        Debug.Log("Player has closed vent door");
        ventCollider.isTrigger = false;
        
    }
}
