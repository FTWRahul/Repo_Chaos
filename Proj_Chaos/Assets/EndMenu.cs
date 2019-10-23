using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void OnDisable()
    {
        quitButton.onClick.RemoveListener(HandleQuitClicked);
    }
    
    
    private void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
