using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveCollectable : Objective
{
    [SerializeField] public bool isCollected;

    public override void Initialization()
    {
        base.Initialization();
        gameManager.AddListenerCollectableEvent(EventInvoke);
        gameManager.AddListenerPlayerRespawn(ResetObjective);
        gameManager.AddListenerLevelFinishedEvent(ConfirmObjective);
    }

    protected override void EventInvoke()
    {
        base.EventInvoke();
        isCollected = true;
        success = true;
    }

    protected override void ResetObjective()
    {
        base.ResetObjective();
        if (!objectiveDone)
        {
            isCollected = false;
            success = false;
        }
    }
}
