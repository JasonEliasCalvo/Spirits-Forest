using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DemonBoss : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2D;

    public int health;
    public int healthMax;
    public bool isDead = false;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject enemyBullet;
    [SerializeField]
    private Transform[] shootController;

    private bool facingRight = true;
    private BossManager bossManager;

    [Header("Boss Health UI")]
    [SerializeField]
    private Image[] bossHealthBarFill;
    [SerializeField]
    private Sprite[] bossHealthBarSprites;
    private int clampedBossHealth;

    [Header("Movement Points")]
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float speed;

    private int nextPlataform = 1;
    private bool orderPlatform = true;
    public bool isMoving = false;

    void Start()
    {
        Flip();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2D.isKinematic = true;
        UpdateBossHealthBar();
    }

    public void GetDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        UpdateBossHealthBar();

        if (health > 0)
        {
            animator.SetTrigger("Special");
        }

        if (health <= 0)
        {
            Death();
            AudioManager.Instance.DemonSound();
        }
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;

        if (isMoving)
        {
            MoveBetweenPoints();
        }

        float playerDistance = Vector2.Distance(transform.position, player.position);
    }

    private void MoveBetweenPoints()
    {
        if (orderPlatform && nextPlataform + 1 >= movementPoints.Length)
            orderPlatform = false;
        if (!orderPlatform && nextPlataform <= 0)
            orderPlatform = true;

        if (Vector2.Distance(transform.position, movementPoints[nextPlataform].position) < 0.001f)
        {
            Attack();
            if (orderPlatform)
                nextPlataform += 1;
            else
                nextPlataform -= 1;
        }
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextPlataform].position, speed * Time.deltaTime);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        isMoving = false;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void ShootBullets()
    {
        foreach (Transform shootPosition in shootController)
        {
            Vector3 shootingDirection = (player.position - shootPosition.position).normalized;

            GameObject bullet = Instantiate(enemyBullet, shootPosition.position, shootPosition.rotation);
            bullet.GetComponent<BulletLogic>().SetDirection(shootingDirection);
        }
    }


    private IEnumerator AnimationDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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

    public void Flip()
    {
        if ((player.position.x < transform.position.x && !facingRight) || (player.position.x > transform.position.x && facingRight))
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(AnimationDeath());
    }

    private void Fall()
    {
        rb2D.isKinematic = false;
        rb2D.velocity = new Vector2(0, -1);
    }
}
