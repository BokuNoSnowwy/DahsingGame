using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingLineInteractable : MonoBehaviour, IInteractable
{
    private bool playerHasFinished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DetectPlayer(Movement playerMovement = null)
    {
        StartCoroutine(GameManager.Instance.EndLevel());
        if (!playerHasFinished)
        {
            playerHasFinished = true;
        }
    }
    
    public Vector2 iniPos { get; set; }
    
    public void ResetInteractable()
    {
        throw new System.NotImplementedException();
    }

    public void ListenEventGameManager()
    {
        throw new System.NotImplementedException();
    }

}
