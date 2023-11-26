using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    [HideInInspector] public bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [HideInInspector] public bool isDisabled;

    [Header("Prerequisites")]
    public Transform characterSprite;
    public Animator animator;
    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer trailRenderer;
    private bool hasLanded;

    public ParticleSystem moveDustParticle;
    bool moveDustPlaying;
    public ParticleSystem starParticle;
    public ParticleSystem wallDustParticle;
    bool wallDustPlaying;

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

    // Player parent platform
    private Transform _originalParent;

    [Header("SFX")]
    public AudioClip SFXDash;

    //new input system
    private PlayerControls playerControls;
    private InputAction move;
    private InputAction jump;
    private InputAction dash;
    private InputAction drop;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        jump = playerControls.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
        jump.Disable();
    }

    private IEnumerator Start()
    {
        _originalParent = transform.parent;
        switch (PlayerPrefs.GetInt("StoryItemData", -1))
        {
            case 0:
                UnlockDoubleJump();
                yield return new WaitForSeconds(0.05f);
                ghostCompanion.transform.position = transform.position;
                break;
            case 1:
            case 2:
            case 3:
                UnlockDoubleJump();
                yield return new WaitForSeconds(0.05f);
                ghostCompanion.transform.position = transform.position;
                UnlockDash();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisabled)
        {
            if (isDashing)
            {
                return;
            }

            horizontal = move.ReadValue<Vector2>().x;

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

                if (!hasLanded)
                {
                    hasLanded = true;

                    Sequence landSquash = DOTween.Sequence();

                    landSquash.Append(characterSprite.DOPunchScale(new Vector3(0.2f, -0.2f, 0), 0.05f, 10, 0))
                        .Append(characterSprite.DOScale(Vector3.one, 0.01f));
                }
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
                moveDustPlaying = false;
                hasLanded = false;
                moveDustParticle.Stop();
            }

            if (jump.triggered)
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

                Sequence jumpSquash = DOTween.Sequence();

                jumpSquash.Append(characterSprite.DOScale(Vector3.one, 0.01f))
                    .Append(characterSprite.DOPunchScale(new Vector3(-0.3f, 0.3f, 0), 0.3f, 10, 0));

                jumpBufferCounter = 0f;
            }
            else if (!doubleJump && jump.triggered && !IsWalled() && doubleJumpUnlocked)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                characterSprite.DOPunchScale(new Vector3(-0.3f, 0.3f, 0), 0.3f, 10, 0);
                jumpBufferCounter = 0f;

                doubleJump = true;
                bootsLight.SetActive(false);
                CinemachineShake.Instance.ShakeCamera(3, 0.1f);
                starParticle.Play();
            }

            if (jump.WasReleasedThisFrame() && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                coyoteTimeCounter = 0f;
            }

            OneWay();

            WallSlide();

            WallJump();

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

        //animator
        if (rb.velocity.x != 0 && IsGrounded() && horizontal != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else if (rb.velocity.x == 0 || !IsGrounded())
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (canDash && dashUnlocked && ghostCompanionUnlocked)
        {
            if (gameObject.scene.name != "Base")
            {
                ResetParent();
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Base"));
            }
            StartCoroutine(DashCoroutine());
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
        TilemapCollider2D platformCollider = currentOneWayPlatform.GetComponent<TilemapCollider2D>();

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

            if (!wallDustPlaying)
            {
                wallDustPlaying = true;
                wallDustParticle.Play();
            }
            
        }
        else
        {
            isWallSliding = false;

            if (wallDustPlaying)
            {
                wallDustPlaying = false;
                wallDustParticle.Stop();
            }
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

        if (jump.triggered && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            characterSprite.DOPunchScale(new Vector3(-0.4f, 0.4f, 0), 0.3f, 10, 0);
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

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        CinemachineShake.Instance.ShakeCamera(8, 0.1f);
        SoundManager.Instance.PlaySound(SFXDash);
        starParticle.Play();
        trailRenderer.emitting = true;
        ghostFollow.DashUsedIndicator();
        characterSprite.DOPunchScale(new Vector3(1f, -1f, 0), 0.3f, 10, 0);

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
        ghostFollow.ActivateDashRefreshIndicator();
    }


    //moving platform code
    public void SetParent(Transform newParent)
    {
        _originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = null;
    }
}
