using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostBoss : MonoBehaviour
{
    private Animator animator;

    [Header("Boss Teleport")]
    private Vector3 currentTarget;
    public Transform player;
    public Transform leftPosition;
    public Transform rightPosition;
    public Transform centerPosition;
    public float teleportDistance;

    public GameObject shield;

    private bool isAtRightSide = true;
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

    void Start()
    {
        currentTarget = rightPosition.position;
        transform.position = currentTarget;
        Flip();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateBossHealthBar();
    }

    public void GetDamage(int damage)
    {
        if (isDead) return;

        if(shield.activeSelf)
        {
            shield.SetActive(false);
        }
        else
        {
            health -= damage;
            UpdateBossHealthBar();

            if (health > 0)
            {
                animator.SetTrigger("Teleport");
            }

            if (health <= 0)
            {
                Death();
                AudioManager.Instance.GhostSound();
            }
        }
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;

        float playerDistance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("playerDistance", playerDistance);
    }

    void TeleportToOppositeSide()
    {
        if (isAtRightSide)
        {
            currentTarget = leftPosition.position;
            isAtRightSide = false;
        }
        else
        {
            currentTarget = rightPosition.position;
            isAtRightSide = true;
        }
        transform.position = currentTarget;
        Flip();
    }

    public void TeleportToCenter()
    {
        currentTarget = centerPosition.position;
        transform.position = currentTarget;
        shield.SetActive(true);
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
        if ((player.position.x > transform.position.x && !facingRight) || (player.position.x < transform.position.x && facingRight))
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
}
