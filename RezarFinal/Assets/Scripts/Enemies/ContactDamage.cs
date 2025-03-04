using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyDamage(other);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyDamage(other);
        }
    }

    private void ApplyDamage(Collision2D other)
    {
        Vector2 hitDirection = other.GetContact(0).normal;
        other.gameObject.GetComponent<PlayerStats>().DamagePlayer(damage, hitDirection);
        MenuManager.Instance.ShowPlayerInfo();
    }
}
