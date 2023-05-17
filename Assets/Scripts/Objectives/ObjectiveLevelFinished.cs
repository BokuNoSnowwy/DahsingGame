using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveLevelFinished : Objective
{
    //public bool isLevelFinished;

    public override void Initialization()
    {
        base.Initialization();
        gameManager.AddListenerLevelFinishedEvent(EventInvoke);
    }
    
    public void EventInvoke()
    {
        base.EventInvoke();
        success = true;
        objectiveDone = true;
    }
}
