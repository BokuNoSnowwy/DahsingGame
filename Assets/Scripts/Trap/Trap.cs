using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IInteractable
{
    public Vector2 iniPos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void DetectPlayer(Movement playerMovement)
    {
        //RESTART SCENE
        if (playerMovement.rb.velocity.y <= 0)
        {
            playerMovement.GetComponent<Player>().Die();
        }
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ResetInteractable()
    {
        throw new System.NotImplementedException();
    }
}
