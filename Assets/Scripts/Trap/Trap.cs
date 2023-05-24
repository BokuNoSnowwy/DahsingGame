using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IInteractable
{

    public Vector2 iniPos { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void DetectPlayer(Movement playerMovement)
    {
        //RESTART SCENE
        playerMovement.GetComponent<Player>().Die();
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

    public void ResetInteractable()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
