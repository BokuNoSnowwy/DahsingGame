using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public int life;

    Collider col;
    Renderer rend;

    public Vector2 iniPos { get { return transform.position; } }

    //Get all component
    void Start()
    {
        transform.position = iniPos;
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    //Kill enemy
    protected virtual void Die()
    {
        col.enabled = false;
        rend.enabled = false;
    }

    //Initialize enemy
    protected virtual void StartEnemy()
    {

    }

    //Launch when player collide
    public void DetectPlayer()
    {
        //TO DO check if player is dashing
        //if player dashing

        life--;
        if(life <= 0)
        {
            Die();
        }

        //if player not dashing
        //TO DO kill player
    }

    //Receive first player input and start scene
    public void ListenEventGameManager()
    {
        StartEnemy();
    }

    //Lanch when scene restart
    public void ResetInteractable()
    {
        transform.position = iniPos;
        col.enabled = true;
        rend.enabled = true;
    }
}
