using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [Range(1, 4)]
    public int life = 1;

    Collider col;
    Renderer rend;

    public Vector2 iniPos { get; set; }

    //Get all component
    void Start()
    {
        iniPos = transform.position;
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();

        //TEST
        StartEnemy();
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
        transform.position = iniPos;
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
