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

    private InGameLevelPanel gameLevelPanel;
    private RippleEffect rippleEffect;

    [HideInInspector] public UnityEvent sceneIsLoaded; 

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
        Debug.LogError("Set Player Instance ");
        
        Player playerP = playerInstance.GetComponent<Player>();
        playerP.Initialization();
        playerP.AddListenerFirstDashRespawn(PlayerMadeFirstMove);
        playerP.AddListenerPlayerDie(RespawnPlayer);
        
        sceneIsLoaded.Invoke();
    }
    
    public void RespawnPlayer()
    {
        Debug.LogError("Respawn Player");
        playerRespawnEvent.Invoke();

        isInGame = false;
        timerLevel = 0;
        
        // Move Player 
        if (playerSpawner != null)
        {
            playerInstance.gameObject.SetActive(true);
            playerInstance.gameObject.transform.position = playerSpawner.position;
        }
    }

    public IEnumerator EndLevel()
    {
        Time.timeScale = 0;
        levelFinishedEvent.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        rippleEffect.enabled = false;
        gameLevelPanel.DisplayPanel();
        //Display the stats of the level
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadSceneAsync(LevelSelectorSceneName);
        ResetLevelListeners();
    }

    public void PlayerMadeFirstMove()
    {
        if (!isInGame)
        {
            Debug.Log("PlayerMadeFirstmove");
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
            Debug.Log("SceneIsLoading");
            yield return null;
        }

        Debug.Log("SceneIsLoaded");

        InitLevel();
        
    }

    // Spawn Player and reset timescale
    private void InitLevel()
    {
        timerLevel = 0;
        Time.timeScale = 1;
        SpawnPlayer();
        gameLevelPanel = FindObjectOfType<InGameLevelPanel>(true);
        rippleEffect = FindObjectOfType<RippleEffect>();
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
            Debug.LogError("Get Player");
            return playerInstance.GetComponent<Player>();
        }
    }
}
