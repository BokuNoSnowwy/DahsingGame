using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveEnemies : Objective
{
    public int enemiesCount = 0;
    public int enemiesCountGoal;

    public override void Initialization()
    {
        base.Initialization();
        gameManager.AddListenerEnemiesEvent(EventInvoke);
        gameManager.AddListenerLevelFinishedEvent(ConfirmObjective);
    }
    
    protected override void EventInvoke()
    {
        base.EventInvoke();
        enemiesCount++;
        if (enemiesCount >= enemiesCountGoal)
        {
            success = true;    
        }
    }
    
    protected override void ResetObjective()
    {
        base.ResetObjective();
        if (!objectiveDone)
        {
            enemiesCount = 0;
            success = false;
        }
    }
}
