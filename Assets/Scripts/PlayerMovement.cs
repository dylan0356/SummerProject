using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 3f;
    private float jumpPower = 5f;
    private bool isFacingRight = true;
    private Animator animator;

    public SpriteRenderer spriteRenderer;
    public Sprite standingSprite;
    public Sprite crouchingSprite;
    public BoxCollider2D b;
    public Vector2 standingSize;
    public Vector2 crouchingSize;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start() {
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standingSprite;
        standingSize = b.size;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Crouch");
            spriteRenderer.sprite = crouchingSprite;
            b.size = crouchingSize;
            b.offset = new Vector2(0f, -0.5f);
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Stand");
            spriteRenderer.sprite = standingSprite;
            b.size = standingSize;
            b.offset = new Vector2(0f, 0f);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
