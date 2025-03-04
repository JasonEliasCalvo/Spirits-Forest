using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatBoss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rB2D;
    public Transform player;

    public float moveSpeed = 5f;
    private bool facingRight = true;

    private BossManager bossManager;

    [Header("Boss Health UI")]
    [SerializeField]
    private Image[] bossHealthBarFill;
    [SerializeField]
    private Sprite[] bossHealthBarSprites;
    private int clampedBossHealth;

    public int health;
    public int healthMax;
    public bool isDead = false;

    [Header("Attack")]
    [SerializeField]
    private Transform hitController;
    [SerializeField]
    private float hitRadius;
    [SerializeField]
    private int hitDamage;

    [Header("fire")]
    public Vector3 shootingDirection;
    [SerializeField]
    private Transform shootController;
    [SerializeField]
    private GameObject enemyBullet;

    void Start()
    {
        animator = GetComponent<Animator>();
        rB2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GetDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        UpdateBossHealthBar();

        if (health <= 0)
        {
            Death();
            AudioManager.Instance.CatSound();
        }
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;
        float playerDistance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("playerDistance", playerDistance);
    }

    private void UpdateBossHealthBar()
    {
        clampedBossHealth = Mathf.Clamp(health, 0, healthMax);

        if (clampedBossHealth == 0)
        {
            foreach (Image healthBarFill in bossHealthBarFill)
            {
                Destroy(healthBarFill.gameObject);
            }
            return;
        }

        if (clampedBossHealth > 0 && clampedBossHealth <= bossHealthBarSprites.Length)
        {
            bossHealthBarFill[0].sprite = bossHealthBarSprites[clampedBossHealth - 1];
        }
        else
        {
            return;
        }
        if (health < 10)
        {
            StartCoroutine(AnimDamage());
        }
    }

    public IEnumerator AnimDamage()
    {
        for (int i = 0; i < 6; i++)
        {
            bossHealthBarFill[1].enabled = !bossHealthBarFill[1].enabled;
            yield return new WaitForSeconds(0.07f);
        }
        if (clampedBossHealth > 0 && clampedBossHealth <= bossHealthBarSprites.Length)
        {
            bossHealthBarFill[1].sprite = bossHealthBarSprites[clampedBossHealth - 1];
        }
        else
        {
            yield break;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(enemyBullet, shootController.position, shootController.rotation);
        bullet.GetComponent<BulletLogic>().SetDirection(shootingDirection);
    }

    public void Flip()
    {
        if ((player.position.x > transform.position.x && !facingRight) || (player.position.x < transform.position.x && facingRight))
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.position, hitRadius);

        foreach (Collider2D colision in objects)
        {
            if (colision.CompareTag("Player"))
            {
                Vector2 hitDirection = (colision.transform.position - transform.position).normalized;
                colision.GetComponent<PlayerStats>().DamagePlayer(hitDamage, hitDirection);
                MenuManager.Instance.ShowPlayerInfo();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitController.position, hitRadius);
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(AnimationDeath());
    }

    private IEnumerator AnimationDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
