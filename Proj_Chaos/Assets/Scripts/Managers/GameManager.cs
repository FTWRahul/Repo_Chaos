using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MENU,
        PREGAME,
        RUNNING,
        QUEST,
        PAUSED,
        END
    }
    
    [HideInInspector] public Events.EventGameState OnGameStateChanged; //Event on change game state

    public string mainLevelName;
    public string mainMenuName;
    public GameObject[] systemPrefabs; //List of the managers need to instantiate
    
    [SerializeField] private GameState currentGameState = GameState.MENU; //Current game state
    public GameState CurrentGameState 
    {
        get => currentGameState;
    }
    
    private List<GameObject> _instancedSystemPrefabs = new List<GameObject>(); //List of instances managers
    private List<AsyncOperation> _asyncOperations = new List<AsyncOperation>();
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();
        UpdateState(GameState.MENU);
    }
    
    private void InstantiateSystemPrefabs()
    {
        foreach (var prefab in systemPrefabs)
        {
            var go = Instantiate(prefab);
            _instancedSystemPrefabs.Add(go);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.RUNNING || currentGameState == GameState.PAUSED)
            {
                TogglePause();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(currentGameState == GameState.RUNNING || currentGameState == GameState.QUEST)
            {
                ToggleList();
            }
        }
        
    }

    public void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.MENU:
                Time.timeScale = 1f;
                Cursor.visible = true;
                break;
                
            case GameState.PREGAME:
                Time.timeScale = 0f;
                break;
            
            case GameState.RUNNING:
                Time.timeScale = 1f;
                Cursor.visible = false;
                break;
            
            case GameState.QUEST:
                Time.timeScale = 1f;
                break;
            
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            
            case GameState.END:
                Time.timeScale = 0.0f;
                Cursor.visible = true;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        OnGameStateChanged.Invoke(previousGameState, currentGameState);
    }

    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene() - blocking call
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);
        
        if(ao == null) 
        {
            Debug.LogWarning("[GameManager] Unable to load level " + levelName);
            return;
        }
        
        ao.completed += OnLoadOperationComplete;
        _asyncOperations.Add(ao);
    }
    
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_asyncOperations.Contains(ao))
        {
            _asyncOperations.Remove(ao);

            if (_asyncOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }
        
        Debug.Log("Load complete");
    }

    
    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var prefab in _instancedSystemPrefabs)
        {
            Destroy(prefab);
        }
        
        _instancedSystemPrefabs.Clear();
    }
    
    public void StartGame()
    {
        LoadLevel(mainLevelName);
    }
    
    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }
    
    private void ToggleList()
    {
        UpdateState(currentGameState == GameState.RUNNING? GameState.QUEST: GameState.RUNNING);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        StartGame();
        UpdateState(GameState.RUNNING);
    }
}
