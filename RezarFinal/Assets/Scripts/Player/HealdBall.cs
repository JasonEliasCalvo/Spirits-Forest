using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealdBall : MonoBehaviour
{
    [SerializeField]
    private int heal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().HealPlayer(heal);
            MenuManager.Instance.ShowPlayerInfo();
            AudioManager.Instance.heal();
            Destroy(gameObject);
        }
    }
}
