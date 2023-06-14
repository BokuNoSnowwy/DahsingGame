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

    [Header("Game")]
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;
    private Transform playerSpawner;
    private cameraFollow cameraFollow;

    private InGameLevelPanel gameLevelPanel;
    private RippleEffect rippleEffect;

    [HideInInspector] public UnityEvent sceneIsLoaded; 

    [Header("Other")] 
    [SerializeField] private bool isInGame;
    [SerializeField] private bool isPaused;
    public bool isInTuto;

    public int lvlUnlock;

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

    private void Start()
    {
        SavingService.LoadGame("LevelsData.json");
        SavingService.LoadGame("Settings.json");
    }

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
    
    public void AddListenerSceneIsLoaded(UnityAction action)
    {
        sceneIsLoaded.AddListener(action);
    }

    #endregion


    #region InGame

    public void SpawnPlayer()
    {
        // Get Spawner position
        playerSpawner = GameObject.FindGameObjectWithTag("Spawner").transform;
        
        GameObject player = Instantiate(playerPrefab,playerSpawner.position, Quaternion.identity);
        playerInstance = player;

        Player playerP = playerInstance.GetComponent<Player>();
        playerP.Initialization();
        playerP.AddListenerFirstDashRespawn(PlayerMadeFirstMove);
        playerP.AddListenerPlayerDie(RespawnPlayer);
        
        sceneIsLoaded.Invoke();
    }
    
    public void RespawnPlayer()
    {
        playerRespawnEvent.Invoke();

        isInGame = false;
        timerLevel = 0;
        cameraFollow.rect.position = cameraFollow.firstPos;
        // Move Player 
        if (playerSpawner != null)
        {
            Player.RespawnPlayer(playerSpawner.position);
        }
    }

    public IEnumerator EndLevel()
    {
        Time.timeScale = 0;
        levelFinishedEvent.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        rippleEffect.enabled = false;
        gameLevelPanel.DisplayPanel();
        playerMovement.rb.gravityScale = playerMovement.gravity;

        if(lvlUnlock >= levelList.IndexOf(GetActualLevel()))
            lvlUnlock = levelList.IndexOf(GetActualLevel()) + 1;
    }

    public void ReturnToLobby()
    {
        StartCoroutine(ReturnToLobbyAsync());
        ResetLevelListeners();
    }

    private IEnumerator ReturnToLobbyAsync()
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(LevelSelectorSceneName);
        while (!asyncLoadLevel.isDone)
        {
            //LoadingScene
            yield return null;
        }
        SavingService.SaveGame("LevelsData.json");
    }

    public void PlayerMadeFirstMove()
    {
        if (!isInGame)
        {
            isInGame = true;
        }
    }

    #endregion

    #region SceneLoading

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GetActualLevel().sceneName.ToString());

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // Scene is loading
            yield return null;
        }
        // Scene is loaded
        InitLevel();
        
    }

    // Spawn Player and reset timescale
    private void InitLevel()
    {
        timerLevel = 0;
        Time.timeScale = 1;
        SpawnPlayer();

        Level myLevel = GetActualLevel();
        if (myLevel.hasTutorial && !myLevel.tutorialCompleted)
        {
            isInTuto = true;
            Instantiate(myLevel.tutorialPrefab);
        }

        gameLevelPanel = FindObjectOfType<InGameLevelPanel>(true);
        rippleEffect = FindObjectOfType<RippleEffect>();
        cameraFollow = Camera.main.gameObject.GetComponent<cameraFollow>();
    }
    
    #endregion

    public Level GetActualLevel()
    {
        return levelList[indexLevel];
    }

    public void LaunchLevel()
    {
        //Update Listeners
        ResetLevelListeners();
        GetActualLevel().SetupObjectives();

        StartCoroutine(LoadYourAsyncScene());
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
    
    public Player Player
    {
        get
        {
            return playerInstance.GetComponent<Player>();
        }
    }
    public Movement playerMovement
    {
        get
        {
            return playerInstance.GetComponent<Movement>();
        }
    }

}
