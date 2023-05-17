using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public bool dashAvailable => !movementScript.hasDashed;

    public bool isAlive;

    [SerializeField] 
    private Movement movementScript;

    [HideInInspector] public UnityEvent firstDashRespawn;
    [HideInInspector] public UnityEvent playerDie;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialization()
    {
        movementScript.dashEvent.AddListener(() =>
        {
            firstDashRespawn.Invoke();
        });

        isAlive = true;
    }

    [ContextMenu("Player Die")]
    private void Die()
    {
        Debug.LogError("Die");
        //Disable the player before respawning to make sure the player won't do unnecessary moves  
        gameObject.SetActive(false);
        playerDie.Invoke();
        
    }

    public void AddListenerFirstDashRespawn(UnityAction action)
    {
        firstDashRespawn.AddListener(action);
    }
    
    public void AddListenerPlayerDie(UnityAction action)
    {
        playerDie.AddListener(action);
    }
}
