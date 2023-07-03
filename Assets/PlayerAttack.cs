using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Debug.Log("Attack");
            Attack();
        }

        if (attacking) {
            timer += Time.deltaTime;
            if (timer >= timeToAttack) {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(false);
                
            }
        }
    }

    private void Attack() {
        attacking = true;
        attackArea.SetActive(true);
    }
}
