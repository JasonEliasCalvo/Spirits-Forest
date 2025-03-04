using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D platformCollider;
    private Vector3 initialPosition;
    private bool isFalling = false;

    [SerializeField]
    private float fallDelay;
    [SerializeField]
    private float resetDelay;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        platformCollider = GetComponent<BoxCollider2D>();
        rb2D.isKinematic = true;
        rb2D.freezeRotation = true;
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    private IEnumerator FallAfterDelay()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);

        rb2D.isKinematic = false;
        platformCollider.enabled = false;
        rb2D.velocity = new Vector2(0, -1);

        yield return new WaitForSeconds(resetDelay);

        StartCoroutine(ResetPlatform());
    }

    private IEnumerator ResetPlatform()
    {
        rb2D.isKinematic = true;
        platformCollider.enabled = true;
        rb2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);

        transform.position = initialPosition;
        isFalling = false;
    }
}
