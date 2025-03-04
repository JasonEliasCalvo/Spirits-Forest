using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [SerializeField] private Transform hitController;
    [SerializeField] private float hitRadius;
    [SerializeField] private int hitDamage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float timeNextAttack;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (timeNextAttack > 0)
        {
            timeNextAttack -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && timeNextAttack <= 0)
        {
            animator.SetTrigger("Attack");
            AudioManager.Instance.Attack();
            timeNextAttack = timeBetweenAttacks;
        }
    }

    public void Hit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.position, hitRadius);

        foreach (Collider2D collider2D in objects)
        {
            if (collider2D.CompareTag("Enemy"))
            {
                collider2D.transform.GetComponent<Enemy>().GetDamage(hitDamage);
            }
            if (collider2D.CompareTag("Boss"))
            {
                CatBoss catBoss = collider2D.GetComponent<CatBoss>();
                if (catBoss != null)
                {
                    catBoss.GetDamage(hitDamage);
                }
                GhostBoss ghostBoss = collider2D.GetComponent<GhostBoss>();
                if (ghostBoss != null)
                {
                    ghostBoss.GetDamage(hitDamage);
                }
                DemonBoss demonBoss = collider2D.GetComponent<DemonBoss>();
                if (demonBoss != null)
                {
                    demonBoss.GetDamage(hitDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitController.position, hitRadius);
    }
}
