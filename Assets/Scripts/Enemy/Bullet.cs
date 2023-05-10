using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Enemy
{
    [SerializeField] float deathDelay;
    [SerializeField] float moveSpeed;

    void Start()
    {
        Invoke("Die", deathDelay);
    }

    void Update()
    {
        Moving();
    }

    void Moving()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    protected override void StartEnemy()
    {
        
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
