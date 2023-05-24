using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveCollectable : Objective
{
    public bool isCollected;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Color baseSpriteColor;
    [SerializeField] private Sprite obtainedSprite;
    [SerializeField] private Color obtainedSpriteColor;
    
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
    
    public Sprite BaseSprite => baseSprite;

    public Color BaseSpriteColor => baseSpriteColor;

    public Sprite ObtainedSprite => obtainedSprite;

    public Color ObtainedSpriteColor => obtainedSpriteColor;
}
