using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float jumpForce = 16f;
    private bool isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool doubleJump;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.3f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [HideInInspector] public bool isDisabled;

    [Header("Prerequisites")]
    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    public ParticleSystem moveDustParticle;
    bool moveDustPlaying;
    public ParticleSystem starParticle;

    public GameObject bootsLight;
    public GameObject ghostCompanion;
    public GhostFollow ghostFollow;

    private GameObject currentOneWayPlatform;

    [SerializeField] CapsuleCollider2D playerCollider;

    //Unlock Player Abilities
    [Header("Unlock Player Abilities")]
    public bool ghostCompanionUnlocked = false;
    public bool doubleJumpUnlocked = false;
    public bool dashUnlocked = false;

    //Player check point
    [HideInInspector] public Transform SpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (!isDisabled)
        {
            if (isDashing)
            {
                return;
            }

            horizontal = Input.GetAxisRaw("Horizontal");

            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
                doubleJump = false;

                if (doubleJumpUnlocked)
                {
                    bootsLight.SetActive(true);
                }
                else
                {
                    bootsLight.SetActive(false);
                }

                if (horizontal != 0 && !moveDustPlaying)
                {
                    moveDustPlaying = true;
                    moveDustParticle.Play();
                }
                else if (horizontal == 0 && moveDustPlaying)
                {
                    moveDustPlaying = false;
                    moveDustParticle.Stop();
                }
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
                moveDustPlaying = false;
                moveDustParticle.Stop();
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            // Jump Button Mechanic 

            if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                jumpBufferCounter = 0f;
            }
            else if (!doubleJump && Input.GetButtonDown("Jump") && !IsWalled() && doubleJumpUnlocked)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter = 0f;

                doubleJump = true;
                bootsLight.SetActive(false);
                CinemachineShake.Instance.ShakeCamera(5, 0.1f);
                starParticle.Play();
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                coyoteTimeCounter = 0f;
            }

            OneWay();

            WallSlide();

            WallJump();

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashUnlocked && ghostCompanionUnlocked)
            {
                StartCoroutine(Dash());
            }

            if (!isWallJumping)
            {
                Flip();
            }

            if (ghostCompanionUnlocked)
            {
                ghostCompanion.SetActive(true);

                if (!dashUnlocked)
                {
                    ghostFollow.ghostLight.SetActive(false);
                }
            }
            else
            {
                ghostCompanion.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!isWallJumping && !isDisabled)
        {
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //One Way Platform mechanic

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }

        // check dead
        if(collision.gameObject.name == "Dead")
        {
            // Get Checkpoint script
            CheckPoint checkPointScript = FindObjectOfType<CheckPoint>();
            checkPointScript.spawnPlayer();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OneWay()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if (currentOneWayPlatform != null) 
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    //Wall Jump Mechanic

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide ()
    {
        if(IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2 (rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping ()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        CinemachineShake.Instance.ShakeCamera(8, 0.1f);
        starParticle.Play();
        trailRenderer.emitting = true;
        ghostFollow.DashUsedIndicator();

        yield return new WaitForSeconds(dashingTime);

        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        ghostFollow.DashRefreshIndicator();
    }


    //unlock abilities
    public void UnlockDoubleJump()
    {
        ghostCompanionUnlocked = true;
        doubleJumpUnlocked = true;
    }

    public void UnlockDash()
    {
        dashUnlocked = true;
        ghostFollow.DashRefreshIndicator();
    }
}
