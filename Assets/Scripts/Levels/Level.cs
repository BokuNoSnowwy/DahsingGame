using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public enum SceneName
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    LevelABCD,
    LevelABCDE,
    LevelD,
    LevelF,
    Level11,
}

[Serializable]
public class Level
{
    public bool objectivesAreViables;
    public SceneName sceneName;
    public int nbDeath;

    [Header("Objectives Management")] 
    public ObjectiveEnemies[] objectivesEnemiesArray;
    public ObjectiveTimer[] objectivesTimerArray;
    public ObjectiveLevelFinished objectiveLevelFinished;
    public ObjectiveCollectable objectiveCollectable;

    [Header("Tutorial")]
    public bool hasTutorial;
    public GameObject tutorialPrefab;

    public void SetupObjectives()
    {
        foreach (var objective in GetObjectives())
        {
            objective.Initialization();
        }
    }

    public  List<Objective> GetObjectives()
    {
        List<Objective> objectivesToReturn = new List<Objective>();
        
        if (objectiveCollectable.IsEnabled)
            objectivesToReturn.Add(objectiveCollectable);
        if (objectiveLevelFinished.IsEnabled)
            objectivesToReturn.Add(objectiveLevelFinished);
        foreach (var enemy in objectivesEnemiesArray)
        {
            if (enemy.IsEnabled)
                objectivesToReturn.Add(enemy);
        }
        foreach (var timer in objectivesTimerArray)
        {
            if (timer.IsEnabled)
                objectivesToReturn.Add(timer);
        }

        return objectivesToReturn;
    }
}
