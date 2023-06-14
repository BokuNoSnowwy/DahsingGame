using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class LevelsSaver : SaveableBehaviour
{
    private const string LOCAL_LEVELS_UNLOCK = "levelsUnlock";

    private const string LOCAL_LEVELS_KEY = "levelsObjective";

    private const string LOCAL_TUTORIAL_COMPLETE = "levelTutoComplete";

    private const string LOCAL_INDEX_KEY = "levelIndex";
    private const string LOCAL_DEATH_KEY = "numberOfDeath";
    private const string LOCAL_COLLECTABLE_KEY = "collectabeDone";
    private const string LOCAL_FINISHED_KEY = "finishedDone";

    private const string LOCAL_ENEMIES_KEY = "enemyObjective";
    private const string LOCAL_ENEMY_ID_KEY = "enemyId";
    private const string LOCAL_ENEMY_DONE_KEY = "enemyDone";

    private const string LOCAL_TIMERS_KEY = "timerObjective";
    private const string LOCAL_TIMER_ID_KEY = "timerId";
    private const string LOCAL_TIMER_DONE_KEY = "timerDone";
    private const string LOCAL_TIMER_SCORE_KEY = "timerScore";

    private JsonData SerializeValue(object obj)
    {
        return JsonMapper.ToObject(JsonUtility.ToJson(obj));
    }

    private T DeserializeValue<T>(JsonData data)
    {
        return JsonUtility.FromJson<T>(data.ToJson());
    }

    public override JsonData SavedData
    {
        get
        {
            var result = new JsonData();

            result[LOCAL_LEVELS_UNLOCK] = GameManager.Instance.lvlUnlock;

            var levelsObjective = new JsonData();

            int i = 0;
            foreach(Level level in GameManager.Instance.levelList)
            {
                var data = new JsonData();

                data[LOCAL_INDEX_KEY] = i;
                data[LOCAL_DEATH_KEY] = level.nbDeath;
                data[LOCAL_TUTORIAL_COMPLETE] = level.tutorialCompleted;
                data[LOCAL_COLLECTABLE_KEY] = level.objectiveCollectable.ObjectiveDone;
                data[LOCAL_FINISHED_KEY] = level.objectiveLevelFinished.ObjectiveDone;

                //Save Enemies Objectives
                if(level.objectivesEnemiesArray.Length > 0)
                {
                    var enemiesObjectives = new JsonData();

                    int j = 0;
                    foreach (ObjectiveEnemies enemy in level.objectivesEnemiesArray)
                    {
                        var dataEnemy = new JsonData();

                        dataEnemy[LOCAL_ENEMY_ID_KEY] = j;
                        dataEnemy[LOCAL_ENEMY_DONE_KEY] = enemy.ObjectiveDone;

                        j++;

                        enemiesObjectives.Add(dataEnemy);
                    }

                    data[LOCAL_ENEMIES_KEY] = enemiesObjectives;
                }

                //Save Timers Objectives
                if (level.objectivesTimerArray.Length > 0)
                {
                    var timersObjectives = new JsonData();

                    int k = 0;
                    foreach (ObjectiveTimer enemy in level.objectivesTimerArray)
                    {
                        var dataTimer = new JsonData();

                        dataTimer[LOCAL_TIMER_ID_KEY] = k;
                        dataTimer[LOCAL_TIMER_DONE_KEY] = enemy.ObjectiveDone;
                        dataTimer[LOCAL_TIMER_SCORE_KEY] = enemy.timerPlayerScore;

                        k++;

                        timersObjectives.Add(dataTimer);
                    }

                    data[LOCAL_TIMERS_KEY] = timersObjectives;
                }

                i++;

                levelsObjective.Add(data);
            }

            result[LOCAL_LEVELS_KEY] = levelsObjective;

            return result;
        }
    }

    public override void LoadFromData(JsonData data)
    {
        if (data.ContainsKey(LOCAL_LEVELS_UNLOCK))
        {
            GameManager.Instance.lvlUnlock = (int)data[LOCAL_LEVELS_UNLOCK];
        }

        if (data.ContainsKey(LOCAL_LEVELS_KEY))
        {
            var objects = data[LOCAL_LEVELS_KEY];

            var objectsCount = objects.Count;

            for (int i = 0; i < objectsCount; i++)
            {
                var objectData = objects[i];
                int levelIndex = 0;

                if (objectData.ContainsKey(LOCAL_INDEX_KEY))
                {
                    levelIndex = (int)objectData[LOCAL_INDEX_KEY];
                }
                if (objectData.ContainsKey(LOCAL_DEATH_KEY))
                {
                    GameManager.Instance.levelList[levelIndex].nbDeath = (int)objectData[LOCAL_DEATH_KEY];
                }
                if (objectData.ContainsKey(LOCAL_TUTORIAL_COMPLETE))
                {
                    GameManager.Instance.levelList[levelIndex].tutorialCompleted = (bool)objectData[LOCAL_TUTORIAL_COMPLETE];
                }
                if (objectData.ContainsKey(LOCAL_COLLECTABLE_KEY))
                {
                    GameManager.Instance.levelList[levelIndex].objectiveCollectable.ObjectiveDone = (bool)objectData[LOCAL_COLLECTABLE_KEY];
                }
                if (objectData.ContainsKey(LOCAL_FINISHED_KEY))
                {
                    GameManager.Instance.levelList[levelIndex].objectiveLevelFinished.ObjectiveDone = (bool)objectData[LOCAL_FINISHED_KEY];
                }

                //Load Enemies Objectives
                if (objectData.ContainsKey(LOCAL_ENEMIES_KEY))
                {
                    var enemiesObjects = objectData[LOCAL_ENEMIES_KEY];

                    var enemiesCount = enemiesObjects.Count;

                    for (int j = 0; j < enemiesCount; j++)
                    {
                        var enemyData = enemiesObjects[j];
                        int enemyIndex = 0;

                        if (enemyData.ContainsKey(LOCAL_ENEMY_ID_KEY))
                        {
                            enemyIndex = (int)enemyData[LOCAL_ENEMY_ID_KEY];
                        }
                        if (enemyData.ContainsKey(LOCAL_ENEMY_DONE_KEY))
                        {
                            GameManager.Instance.levelList[levelIndex].objectivesEnemiesArray[enemyIndex].ObjectiveDone = (bool)enemyData[LOCAL_ENEMY_DONE_KEY];
                        }
                    }
                }

                //Load Timer Objectives
                if (objectData.ContainsKey(LOCAL_TIMERS_KEY))
                {
                    var timersObjects = objectData[LOCAL_TIMERS_KEY];

                    var timersCount = timersObjects.Count;

                    for (int j = 0; j < timersCount; j++)
                    {
                        var timerData = timersObjects[j];
                        int timerIndex = 0;

                        if (timerData.ContainsKey(LOCAL_TIMER_ID_KEY))
                        {
                            timerIndex = (int)timerData[LOCAL_TIMER_ID_KEY];
                        }
                        if (timerData.ContainsKey(LOCAL_TIMER_DONE_KEY))
                        {
                            GameManager.Instance.levelList[levelIndex].objectivesTimerArray[timerIndex].ObjectiveDone = (bool)timerData[LOCAL_TIMER_DONE_KEY];
                        }
                        if (timerData.ContainsKey(LOCAL_TIMER_SCORE_KEY))
                        {
                            GameManager.Instance.levelList[levelIndex].objectivesTimerArray[timerIndex].timerPlayerScore = (float)(double)timerData[LOCAL_TIMER_SCORE_KEY];
                        }
                    }
                }
            }
        }
    }
}
