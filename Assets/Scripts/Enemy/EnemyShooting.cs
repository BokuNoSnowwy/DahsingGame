using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    //[SerializeField] GameObject bulletPrefab;

    [SerializeField] float shootRate;
    [SerializeField] Transform shootPoint;

    bool isShooting;

    [SerializeField] ObjectPool pool;

    //Shoot bullet
    IEnumerator Shooting()
    {
        while (isShooting)
        {
            GameObject bullet = pool.GetObject()/*Instantiate(bulletPrefab, shootPoint.position, transform.rotation)*/;
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = transform.rotation;
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
