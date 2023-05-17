using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveCollectable : Objective
{
    public bool isCollected;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite obtainedSprite;

    private SpriteRenderer spriteRenderer;

    public override void Initialization()
    {
        base.Initialization();
        gameManager.AddListenerCollectableEvent(EventInvoke);
        gameManager.AddListenerPlayerRespawn(ResetObjective);
        gameManager.AddListenerLevelFinishedEvent(ConfirmObjective);
        
        // Collectable Setup
        
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
