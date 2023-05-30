using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [Range(1, 4)]
    public int life = 1;

    Collider2D col;
    Renderer rend;

    public Vector2 iniPos { get; set; }

    //Get all component
    public virtual void Start()
    {
        iniPos = transform.position;
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();

        //TEST
        //StartEnemy();
        
        GameManager.Instance.AddListenerPlayerRespawn(ResetInteractable);
    }

    //Kill enemy
    protected virtual void Die()
    {
        col.enabled = false;
        rend.enabled = false;
        GameManager.Instance.enemiesEvent.Invoke();
    }

    //Initialize enemy
    protected virtual void StartEnemy()
    {
        transform.position = iniPos;
    }

    //Launch when player collide
    public void DetectPlayer(Movement playerMovement)
    {
        if (playerMovement.isDashing)
        {
            life--;
            if (life <= 0)
            {
                Die();
            }
            playerMovement.hasDashed = false;
        }
        else
        {
            //RESTART SCENE
            playerMovement.GetComponent<Player>().Die();
        }
    }

    //Receive first player input and start scene
    public void ListenEventGameManager()
    {
        
    }

    //Lanch when scene restart
    public virtual void ResetInteractable()
    {
        transform.position = iniPos;
        col.enabled = true;
        rend.enabled = true;
    }
}
