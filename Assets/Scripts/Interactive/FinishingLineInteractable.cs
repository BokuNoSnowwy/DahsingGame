using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingLineInteractable : MonoBehaviour, IInteractable
{
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
        Debug.LogError("DetectPlayer");
        StartCoroutine(GameManager.Instance.EndLevel());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DetectPlayer();
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
