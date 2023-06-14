using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapToutAzimut : MonoBehaviour, IInteractable
{
    public Vector2 iniPos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void DetectPlayer(Movement playerMovement)
    {
        playerMovement.GetComponent<Player>().Die();
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
