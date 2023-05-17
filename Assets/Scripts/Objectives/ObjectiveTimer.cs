using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectiveTimer : Objective
{
    public float timerLevel;
    public float timerPlayerScore;

    public override void Initialization()
    {
        base.Initialization();
        gameManager.AddListenerLevelFinishedEvent(EventInvoke);
    }
    
    public void EventInvoke()
    {
        base.EventInvoke();
        if (gameManager.TimerLevel < timerLevel)
        {
            success = true;
            objectiveDone = true;
        }
        
        timerPlayerScore = (timerPlayerScore < gameManager.TimerLevel && timerPlayerScore != 0) ? timerPlayerScore : gameManager.TimerLevel;
    }
    
}
