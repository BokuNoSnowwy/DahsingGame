using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : Enemy
{
    [SerializeField] Vector2 pos1;
    [SerializeField] Vector2 pos2;

    bool isMoving;
    bool hasStartedMoving;

    [SerializeField] float moveSpeed;

    
    public override void Start()
    {
        base.Start();
        GameManager.Instance.AddListenerSceneIsLoaded(PlayerStartEnemy);
    }

    private void PlayerStartEnemy()
    {
        GameManager.Instance.Player.AddListenerFirstDashRespawn(StartEnemy);
    }
    
    //Move enemy between pos1 and pos2
    IEnumerator Moving()
    {
        Vector2 target = pos1;
        while (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, iniPos + target, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, iniPos + target) <= 0)
            {
                target = target == pos1 ? pos2 : pos1;
                yield return new WaitForSeconds(.1f); //wait for .1s when reach a pos
            }
            yield return null;
        }
    }

    //Kill enemy
    protected override void Die()
    {
        isMoving = false;
        base.Die();
    }

    //Initialize enemy
    protected override void StartEnemy()
    {
        base.StartEnemy();

        if (!hasStartedMoving)
        {
            isMoving = true;
            StartCoroutine(Moving());
        }
        
        hasStartedMoving = true;

    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
        isMoving = false;
        hasStartedMoving = false;
    }
}
