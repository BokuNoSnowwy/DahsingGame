using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : Enemy
{
    [SerializeField] Vector2 pos1;
    [SerializeField] Vector2 pos2;

    bool isMoving;

    [SerializeField] float moveSpeed;

    void Moving()
    {

    }

    protected override void Die()
    {
        isMoving = false;
        transform.position = pos1;
        base.Die();
    }
}
