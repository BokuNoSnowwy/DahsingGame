using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Enemy, IObjectPoolNotifier
{
    [SerializeField] float deathDelay;
    [SerializeField] float moveSpeed;

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
        gameObject.ReturnToPool();
        CancelInvoke();
    }

    public void OnEnqueuedToPool()
    {
        
    }

    public void OnCreateOrDequeuedFromPool(bool created)
    {
        Invoke("Die", deathDelay);
    }

    public override void ResetInteractable()
    {
        //base.ResetInteractable();
        Die();
    }
}
