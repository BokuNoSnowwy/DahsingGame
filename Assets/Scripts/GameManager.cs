using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string LevelSelectorSceneName;
    
    [Header("UnityEvents")] 
    [HideInInspector] public UnityEvent collectableEvent;
    [HideInInspector] public UnityEvent enemiesEvent;
    [HideInInspector] public UnityEvent levelFinishedEvent;

    [HideInInspector] public UnityEvent playerRespawnEvent;

    [Header("Levels")]
    [SerializeField] private int indexLevel;

    public List<Level> levelList = new List<Level>();

    private float timerLevel;

    [Header("Other")] 
    [SerializeField] private bool isInGame;
    [SerializeField] private bool isPaused;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && isInGame)
        {
            timerLevel += Time.deltaTime;
        }
    }

    #region Listeners
    
    public void AddListenerCollectableEvent(UnityAction action)
    {
        collectableEvent.AddListener(action);
    }
    
    public void AddListenerEnemiesEvent(UnityAction action)
    {
        enemiesEvent.AddListener(action);
    }
    
    public void AddListenerLevelFinishedEvent(UnityAction action)
    {
        levelFinishedEvent.AddListener(action);
    }

    public void AddListenerPlayerRespawn(UnityAction action)
    {
        playerRespawnEvent.AddListener(action);
    }

    #endregion


    #region InGame

    public void RespawnPlayer()
    {
        playerRespawnEvent.Invoke();
    }

    public void EndLevel()
    {
        levelFinishedEvent.Invoke();
        
        //Display the stats of the level
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadSceneAsync(LevelSelectorSceneName);
        ResetLevelListeners();
    }

    public void PlayerMadeFirstMove()
    {
        isInGame = true;
    }

    #endregion

    public Level GetActualLevel()
    {
        return levelList[indexLevel];
    }

    public void LaunchLevel()
    {
        ResetLevelListeners();
        SceneManager.LoadSceneAsync(GetActualLevel().sceneName.ToString());
        GetActualLevel().SetupObjectives();
    }

    private void ResetLevelListeners()
    {
        levelFinishedEvent.RemoveAllListeners();
        playerRespawnEvent.RemoveAllListeners();
        enemiesEvent.RemoveAllListeners();
        collectableEvent.RemoveAllListeners();
    }

    // Check if only 3 objectives are selected
    private void OnValidate()
    {
        foreach (var level in levelList)
        {
            int nbObjectives = 0;
            if (level.objectiveCollectable.IsEnabled)
                nbObjectives++;
            if (level.objectiveLevelFinished.IsEnabled)
                nbObjectives++;
            foreach (var enemy in level.objectivesEnemiesArray)
            {
                if (enemy.IsEnabled)
                    nbObjectives++;
            }
            foreach (var timer in level.objectivesTimerArray)
            {
                if (timer.IsEnabled)
                    nbObjectives++;
            }

            level.objectivesAreViables = nbObjectives == 3;
        }
    }

    public float TimerLevel { get => timerLevel; }

    public int IndexLevel { get => indexLevel; set => indexLevel = value; }
}
