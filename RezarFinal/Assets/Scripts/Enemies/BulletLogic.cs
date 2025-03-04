using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

    private Vector3 bulletDirection;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * bulletDirection);
    }

    public void SetDirection(Vector3 direction)
    {
        bulletDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<PlayerStats>().DamagePlayer(damage, hitDirection);
            MenuManager.Instance.ShowPlayerInfo();
            Destroy(gameObject);
        }
    }
}
