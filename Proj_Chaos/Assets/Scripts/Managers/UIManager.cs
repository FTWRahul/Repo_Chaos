using UnityEngine;

public enum EndType
{
    NONE,
    WIN,
    FAIL
}

public class UIManager : Singleton<UIManager>
{
    public EndType currentEndType = EndType.NONE;
    
    [SerializeField] private GameObject preGameMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private QuestMenu questMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endMenu;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        questMenu.onQuestMenuMove.AddListener(AudioManager.Instance.PlayQuestMenuClip);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    
    private void HandleGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        Debug.Log(currentState);
        preGameMenu.gameObject.SetActive(currentState == GameManager.GameState.PREGAME);
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        mainMenu.SetActive(currentState == GameManager.GameState.MENU);
        endMenu.gameObject.SetActive(currentState == GameManager.GameState.END);
        
        if (currentState == GameManager.GameState.QUEST || currentState == GameManager.GameState.RUNNING)
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
    }
}
