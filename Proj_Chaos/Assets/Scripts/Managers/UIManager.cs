using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Events.EventFadeComplete onMainMenuFadeComplete;
    
    [SerializeField] private PreGameMenu preGameMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private QuestMenu questMenu;
    [SerializeField] private MainMenu mainMenu;
     public EndMenu endMenu;
    [SerializeField] private Camera dummyCamera;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged.RemoveListener(HandleGameStateChanged);
    }


    private void Update()
    {
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
        }
    }
    
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        mainMenu.gameObject.SetActive(currentState == GameManager.GameState.MENU);
        endMenu.gameObject.SetActive(currentState == GameManager.GameState.END);
        
        if (currentState == GameManager.GameState.OPENLIST || currentState == GameManager.GameState.RUNNING)
        {
            questMenu.gameObject.SetActive(true);
        }
        else
        {
            questMenu.gameObject.SetActive(false);
        }
    }

    public void InitQuestMenu()
    {
        questMenu.Init();
    }
    
    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
    }
}
