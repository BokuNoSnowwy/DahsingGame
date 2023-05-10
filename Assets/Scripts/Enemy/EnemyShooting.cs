using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float shootRate;
    [SerializeField] Transform shootPoint;

    bool isShooting;

    //Shoot bullet
    IEnumerator Shooting()
    {
        while (isShooting)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);
            yield return new WaitForSeconds(1 / shootRate);
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
