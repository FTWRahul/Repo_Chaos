using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(HandleRestartClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(HandleRestartClicked);
        quitButton.onClick.RemoveListener(HandleQuitClicked);
    }

    
    private void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
    
    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
