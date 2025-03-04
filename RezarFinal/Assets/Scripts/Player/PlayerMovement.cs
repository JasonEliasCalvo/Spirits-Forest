using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField]
    private Vector2 recoil;

    public float speed;
    private float moveInput;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask isInGround;
    [SerializeField]
    private Transform groundController;
    [SerializeField]
    private Vector3 boxDimension;

    public bool isGrounded;
    private bool facingRight = true;

    private Vector3 plataformSpeed;
    private float platformInfluence = 0.1f;
    private bool onPlatform;

    public Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.WorldExplored) return;

        moveInput = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Horizontal",Mathf.Abs(moveInput));

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioManager.Instance.Jump();
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundController.position, boxDimension, 0f, isInGround);

        animator.SetBool("IsGrounded", isGrounded);
        
        Move(moveInput);
    }

    private void Move(float move)
    {
        Vector2 targetSpeed = new Vector2(move, rb2D.velocity.y);

        if (onPlatform)
        {
            platformInfluence = Mathf.Lerp(platformInfluence, 1.0f, Time.deltaTime * 4f);

            targetSpeed.x = Mathf.Clamp(move + plataformSpeed.x * platformInfluence, -speed, speed);
        }
        else
        {
            platformInfluence = 0.1f;
        }
        rb2D.velocity = targetSpeed;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void rebound(Vector2 hitDirection)
    {
        AudioManager.Instance.Damege();
        rb2D.velocity = new Vector2(-recoil.x * hitDirection.x, recoil.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            PlataMovement plataform = collision.gameObject.GetComponent<PlataMovement>();
            if (plataform != null)
            {
                plataformSpeed = Vector3.Lerp(plataformSpeed, plataform.GetVelocity(), 0.5f);
                onPlatform = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            plataformSpeed = Vector3.zero;
            onPlatform = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundController.position, boxDimension);
    }
}
