using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator animator;
    private float lastGroundedTime;
    private float lastJumpTime;
    private bool isGrounded;
    public float nextLevel;
    public Animator screenAnimator;
    private bool hasDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(transform.position.y < resetThreshold) {
            StartCoroutine(ScreenWipeAndReset());
            Tracker.Reset();
            screenAnimator.SetTrigger("Fall");
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        isGrounded = IsGrounded();

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            // You can re-enable the animator controls if needed
            // animator.SetBool("IsJumping", false); 
        }
        else
        {
            // animator.SetBool("IsJumping", true); 
        }

        if (rb.velocity.y <= 0.4 && Input.GetButtonDown("Jump") && (Time.time - lastGroundedTime <= coyoteTime))
        {
            lastJumpTime = Time.time;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        Flip(horizontal);
    }

    IEnumerator ScreenWipeAndReset()
    {

        yield return new WaitForSeconds(1f); 

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (normal.y > 0.01)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (Mathf.Abs(normal.y) > 0.5)
            {
                isGrounded = true;
            }
            else 
            {
                if (isGrounded && Mathf.Abs(normal.y) <= 0.5)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -1f);
                    isGrounded = false; 
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Fish")) {
            Destroy(other.gameObject);
            Tracker.Reset();
            SceneManager.LoadScene("Level " + nextLevel);
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private bool IsGrounded()
    {
        if (col == null) return false;
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(col.bounds.center, col.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0f && transform.localScale.x < 0f || horizontal < 0f && transform.localScale.x > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}