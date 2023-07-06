using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private GameObject light;
    private Animator animator;

    private bool attacking = false;
    

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        light = transform.GetChild(3).gameObject;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Attack();
        }
        if (attacking) {
            animator.SetBool("attacking", true);
        } else {
            animator.SetBool("attacking", false);
        }
        if (attacking) {
            timer += Time.deltaTime;
            if (timer >= timeToAttack) {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(false);
                light.SetActive(false);
                
            }
        }
    }

    
    private void Attack() {
        attacking = true;
        attackArea.SetActive(true);
        light.SetActive(true);
    }
}
