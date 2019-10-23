using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animation mainMenuAnimator;
    [SerializeField] private AnimationClip fadeInAnimationClip;
    [SerializeField] private AnimationClip fadeOutAnimationClip;
    
    private void Start()
    {
        mainMenuAnimator = GetComponent<Animation>();
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
        if (currentState == GameManager.GameState.MENU)
        {
            FadeIn();
        }

        if (previousState != GameManager.GameState.MENU && currentState == GameManager.GameState.PREGAME)
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeInAnimationClip;
        mainMenuAnimator.Play();
    }

    private void FadeOut()
    {
        mainMenuAnimator.Stop();
        mainMenuAnimator.clip = fadeOutAnimationClip;
        mainMenuAnimator.Play();
    }
}
