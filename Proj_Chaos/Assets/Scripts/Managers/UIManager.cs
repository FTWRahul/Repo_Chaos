using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Events.EventFadeComplete onMainMenuFadeComplete;
    
    [SerializeField] private PreGameMenu preGameMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private PaperListMenu listMenu;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Camera dummyCamera;

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
        listMenu.gameObject.SetActive(currentState == GameManager.GameState.RUNNING);

        /*if (currentState != GameManager.GameState.PREGAME || currentState != GameManager.GameState.RUNNING)
        {
            preGameMenu.gameObject.SetActive(currentState == GameManager.GameState.PREGAME);
        }*/
    }
    

    public void SetDummyCameraActive(bool active)
    {
        dummyCamera.gameObject.SetActive(active);
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    public void PreGame()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.PREGAME);
    }
}
