using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameMenu : MonoBehaviour
{
    public Events.EventFadeComplete onMainMenuFadeComplete;
    
    private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip fadeInAnimationClip;
    [SerializeField] private AnimationClip fadeOutAnimationClip;

    
    private void Start()
    {
        _mainMenuAnimator = GetComponent<Animation>();
    }
    
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
            FadeOut();
        }

        if (previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            FadeIn();
        }
    }
    
    public void OnFadeOutComplete()
    {
        onMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        onMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = fadeInAnimationClip;
        _mainMenuAnimator.Play();
    }
    
    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);
        
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = fadeOutAnimationClip;
        _mainMenuAnimator.Play();
    }
}
