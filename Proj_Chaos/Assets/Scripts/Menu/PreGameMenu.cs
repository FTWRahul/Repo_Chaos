using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameMenu : MonoBehaviour
{

    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip fadeInAnimationClip;
    [SerializeField] private AnimationClip fadeOutAnimationClip;

    [SerializeField] private GameObject text;
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged.RemoveListener(HandleGameStateChanged);
    }
    
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeIn();
        }
    }
    
    public void OnFadeInComplete()
    {
        /*UIManager.Instance.SetDummyCameraActive(true);*/
        text.SetActive(false);
        LoadLevel();
        FadeOut();
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = fadeInAnimationClip;
        _mainMenuAnimator.Play();
    }
    
    public void FadeOut()
    {
        /*UIManager.Instance.SetDummyCameraActive(false);*/
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = fadeOutAnimationClip;
        _mainMenuAnimator.Play();
    }

    public void LoadLevel()
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
    }
}
