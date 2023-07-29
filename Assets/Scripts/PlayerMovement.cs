using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 15f;
    private bool isFacingRight = true;
    private Animator animator;

    // dash variables
    private Image dashImage;
    [SerializeField] Sprite dashRed;
    [SerializeField] Sprite dashGreen;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    // wall slide and jump variables
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.3f;
    private Vector2 wallJumpingPower = new Vector2(6f, 15f);

    private bool doubleJump;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;


    // sound FX
    [SerializeField] private AudioSource runSound;
    private int runSoundTimer = 0;
    private bool isRunning = false;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource landingSound;
    
    

    void Start() {
        animator = GetComponent<Animator>();
        dashImage = GameObject.Find("DashIndicator").GetComponent<Image>();
    }
    void Update()
    {
        if (!canDash) {
            dashImage.sprite = dashRed;
        } else {
            dashImage.sprite = dashGreen;
        }

        if (isGrounded() && !Input.GetButton("Jump")) {
            doubleJump = false;
        }

        // if dashing dont allow movement
        if (isDashing) {
            animator.SetBool("isDashing", true);
            return;
        } else {
            animator.SetBool("isDashing", false);
        }

        // get left right input
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0f || horizontal < 0f)
        {
            animator.SetBool("isRunning", true);
            isRunning = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }

        if (!isGrounded()) {
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isJumping", false);
        }

        if (isWallSliding) {
            animator.SetBool("isWallSliding", true);
        } else {
            animator.SetBool("isWallSliding", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump) {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                doubleJump = !doubleJump;
                jumpSound.Play();
            }
        }
        
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            StartCoroutine(Dash());
            dashSound.Play();
        }

        // sound FX
        if (isRunning && isGrounded()) {
            if (runSoundTimer == 0) {
                runSoundTimer = 35;
                runSound.Play();
            } else {
                runSoundTimer--;
            }
        }

        WallSlide();
        WallJump();
        
        if (!isWallJumping) {
            Flip();
        }

    }

    void FixedUpdate()
    {
        if (isDashing) {
            animator.SetBool("isDashing", true);
            return;
            } else {
            animator.SetBool("isDashing", false);
        }

        if (!isWallJumping) {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
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

    private bool isWalled() {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
    }

    private void WallSlide() {
        if (isWalled() && !isGrounded() && horizontal != 0f) {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } else {
            isWallSliding = false;
        }
    }

    private void WallJump() {
        if (isWallSliding) {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        } else {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f) {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingPower.x * wallJumpingDirection, wallJumpingPower.y);
            wallJumpingCounter = 0f; // prevents player from spamming jump

            if (transform.localScale.x != wallJumpingDirection) {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping() {
        isWallJumping = false;

    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform") || other.gameObject.CompareTag("Terrain")) {
            landingSound.Play();
        }
    }
}
