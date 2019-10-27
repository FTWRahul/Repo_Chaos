﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(HandleResumeClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }

    private void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }
    
    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
