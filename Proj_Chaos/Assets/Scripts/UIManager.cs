using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Events.EventFadeComplete onMainMenuFadeComplete;
    
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private PaperListMenu listMenu;
    [SerializeField] private Camera dummyCamera;
    
    private void Start()
    {
        mainMenu.onMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        /*listMenu.onMoveList.AddListener(HandleListMoveChanged);*/
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    
    private void HandleMainMenuFadeComplete(bool fadeOut)
    {
        onMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) return;
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }
    
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
        
    }

/*    private void HandleListMoveChanged(bool active)
    {
        listMenu.gameObject.SetActive(active);
    }*/

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }
}
