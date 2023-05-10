using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float shootRate;
    [SerializeField] Vector2 shootPoint;

    bool isShooting;

    //Shoot bullet
    IEnumerator Shooting()
    {
        while (isShooting)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint, Quaternion.identity);
            yield return new WaitForSeconds(shootRate / 1);
        }
    }

    //Kill enemy
    protected override void Die()
    {
        isShooting = false;
        base.Die();
    }

    //Initialize enemy
    protected override void StartEnemy()
    {
        base.StartEnemy();
        isShooting = true;
        StartCoroutine(Shooting());
    }
}
