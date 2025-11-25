using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public ParticleSystem dust;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public float resetThreshold;

    public Transform respawnPosition;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CapsuleCollider2D groundCheck;

    private Animator animator;

    public void SetSpawnPosition(Transform newSpawnPosition)
    {
        respawnPosition.position = newSpawnPosition.position;
    }

    public void TeleportBackToSpawnPoint()
    {
        transform.position = respawnPosition.position;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(transform.position.y < resetThreshold) {
            transform.position = respawnPosition.position;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        } else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Debug.Log("player is, " + groundCheck);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            CreateDust();
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TeleportBackToSpawnPoint();
        }
    }

    IEnumerator ScreenWipeAndReset()
    {

        yield return new WaitForSeconds(1f);

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

    }

    private bool IsGrounded()
    {
        
        if (groundCheck == null) return false;
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(groundCheck.bounds.center, groundCheck.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            if (IsGrounded())
            {
                CreateDust();
            } 
            
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void CreateDust()
    {
        dust.Play();
    }
}

    