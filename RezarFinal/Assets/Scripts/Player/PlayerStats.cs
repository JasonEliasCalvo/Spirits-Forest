using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int healthMax;

    [SerializeField]
    private float recoilTime;

    public Animator animator;
    private PlayerMovement playerMovement;
    private bool isInvulnerable = false;

    public void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void DamagePlayer(int damage, Vector2 hitDirection)
    {
        if (isInvulnerable) return;

        health -= damage;
        animator.SetTrigger("Damage");
        playerMovement.rebound(hitDirection);
        StartCoroutine(LostControl());
        MenuManager.Instance.StartCoroutine(MenuManager.Instance.AnimDamage());

        if (health <= 0)
        {
            PlayerDeath();
            AudioManager.Instance.GameOver();
        }
    }
    public void HealPlayer(int heal)
    {
        if (health >= 5) return;
        health += heal;
        MenuManager.Instance.StartCoroutine(MenuManager.Instance.AnimDamage());
    }

    IEnumerator LostControl()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(recoilTime);
        isInvulnerable = false;
    }

    private void PlayerDeath()
    {
        Destroy(gameObject);
        MenuManager.Instance.GameOver.SetActive(true);
    }
}
