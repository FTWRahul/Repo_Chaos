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
        OPENLIST,
        PAUSED,
        END
    }

    public string mainLevelName;
    public string mainMenuName;
    public GameObject[] systemPrefabs; //List of the managers need to instantiate
    public Events.EventGameState OnGameStateChanged; //Event on change game state
    public GameState CurrentGameState 
    {
        get => currentGameState;
        private set => currentGameState = value;
    }
    
    [SerializeField] private GameState currentGameState = GameState.PREGAME; //Current game state
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
        GameObject go;
        
        foreach (var prefab in systemPrefabs)
        {
            go = Instantiate(prefab);
            _instancedSystemPrefabs.Add(go);
        }
    }
    
    private void Update()
    {
        if(currentGameState == GameState.PREGAME) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (currentGameState == GameState.PAUSED) return;
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleList();
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
                break;
                
            case GameState.PREGAME:
                Time.timeScale = 0f;
                break;
            
            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;
            
            case GameState.OPENLIST:
                Time.timeScale = 1f;
                break;
            
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            
            case GameState.END:
                Time.timeScale = 0.0f;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        OnGameStateChanged.Invoke(currentGameState, previousGameState);
    }

    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene() - blocking call
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        
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
        SceneManager.UnloadSceneAsync(mainMenuName);
        LoadLevel(mainLevelName);
    }
    
    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }
    
    private void ToggleList()
    {
        UpdateState(currentGameState == GameState.RUNNING? GameState.OPENLIST: GameState.RUNNING);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }
}
