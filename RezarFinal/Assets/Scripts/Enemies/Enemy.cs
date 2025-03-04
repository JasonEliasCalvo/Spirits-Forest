using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public bool isDead = false;
    public int health;
    public int healthMax;
    private Collider2D enemyCollider;
    private Rigidbody2D enemyRigidbody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Death();
            enemyRigidbody.bodyType = RigidbodyType2D.Static;
            enemyCollider.enabled = false;
        }
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(AnimationDeath());
    }

    private IEnumerator AnimationDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
