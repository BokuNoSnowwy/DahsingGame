using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public int life;

    public void Die()
    {

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
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
