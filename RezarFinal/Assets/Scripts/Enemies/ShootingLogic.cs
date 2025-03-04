using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField]
    private Transform shootController;
    [SerializeField]
    private float lineDistance;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private bool isPlayerInRange;

    [SerializeField]
    private float timeBetweenShots;
    [SerializeField]
    private float lastShotTime;
    [SerializeField]
    private float shotDelay;
    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip shootSound;

    public Vector3 shootingDirection;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Update()
    {
        isPlayerInRange = Physics2D.Raycast(shootController.position, shootingDirection, lineDistance, playerLayer);

        if (isPlayerInRange)
        {
            if (Time.time > timeBetweenShots + lastShotTime)
            {
                lastShotTime = Time.time;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(enemyBullet, shootController.position, shootController.rotation);
        bullet.GetComponent<BulletLogic>().SetDirection(shootingDirection);
        AudioManager.Instance.EnemySound(shootSound);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(shootController.position, shootController.position + shootingDirection * lineDistance);
    }
}
