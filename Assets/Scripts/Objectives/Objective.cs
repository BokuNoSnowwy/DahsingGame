using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class Objective
{
    [SerializeField] protected bool isEnabled;
    [SerializeField] protected bool success;
    [SerializeField] protected bool objectiveDone;

    protected GameManager gameManager;

    public virtual void Initialization()
    {
        gameManager = GameManager.Instance;
    }

    protected virtual void EventInvoke()
    {
        if (!isEnabled)
        {
            return;
        }
    }

    protected virtual void ResetObjective()
    {
        if (!isEnabled)
        {
            return;
        }
    }
    
    protected virtual void ConfirmObjective()
    {
        if (success)
        {
            objectiveDone = true;
        }
    }


    public bool Success { get => success; set => success = value; }
    public bool ObjectiveDone { get => objectiveDone; set => objectiveDone = value; }

    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
}
