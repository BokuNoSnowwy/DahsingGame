using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public int life;

    Collider col;
    Renderer rend;

    public void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    protected virtual void Die()
    {
        col.enabled = false;
        rend.enabled = false;
    }

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

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public void ResetInteractable()
    {
        col.enabled = true;
        rend.enabled = true;
    }
}
