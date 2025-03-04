using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform groundController;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private LayerMask isInGround;
    [SerializeField]
    private bool facingRight;

    private Rigidbody2D rb2D;
    private bool canFlip = true;
    private Enemy enemy;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D isGrounded = Physics2D.Raycast(groundController.position, Vector2.down, groundDistance);

        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundController.position, facingRight ? Vector2.right : Vector2.left, groundDistance, isInGround);

        Patrol(isGrounded, isGroundAhead);
    }

    private void Patrol(RaycastHit2D isGrounded, RaycastHit2D isGroundAhead)
    {
        if (enemy.isDead) return;

        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);

        if ((!isGrounded || isGroundAhead) && canFlip)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180,0);
        speed *= -1;
        canFlip = false;
        StartCoroutine(FlipCooldown());
    }

    private IEnumerator FlipCooldown()
    {
        yield return new WaitForSeconds(1);
        canFlip = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * groundDistance);
    }
}
