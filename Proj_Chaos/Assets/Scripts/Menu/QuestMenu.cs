using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : Singleton<QuestMenu>
{
    public Events.EventQuestMenuMove onQuestMenuMove;
    
    public List<TextMeshProUGUI> textOrderedList;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform spawnPosition;
    private UITweener _uiTweener;
    
    private void Start()
    {
        _uiTweener = GetComponent<UITweener>();
    }
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged.RemoveListener(HandleGameStateChanged);
    }

    //call after all instan
    public void Init()
    {
        QuestGenerator questGen = FindObjectOfType<PlayerController>().GetComponent<QuestGenerator>();
        foreach (int itemID in questGen.itemsToCollect)
        {
            TextMeshProUGUI itemText = Instantiate(textPrefab, spawnPosition.transform).GetComponent<TextMeshProUGUI>();
            itemText.text = ItemsDatabase.Instance.database[itemID].itemName;
            textOrderedList.Add(itemText);
        }
    }

    
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.QUEST)
        {
            MoveIn();
        }

        if (previousState == GameManager.GameState.QUEST && currentState == GameManager.GameState.RUNNING)
        {
            MoveOut();
        }
    }

    private void MoveIn()
    {
        onQuestMenuMove?.Invoke();
       _uiTweener.HandleTween();
       _uiTweener.SwapDirection();
    }

    private void MoveOut()
    {
        onQuestMenuMove?.Invoke();
        _uiTweener.HandleTween();
        _uiTweener.SwapDirection();
    }

    public void StrikeItem(int id)
    {
        foreach (var textMeshProUgui in textOrderedList)
        {
            if (textMeshProUgui.text.Trim().Equals( ItemsDatabase.Instance.database[id].itemName))
            {
                if (textMeshProUgui.fontStyle != FontStyles.Strikethrough)
                {
                    textMeshProUgui.fontStyle = FontStyles.Strikethrough;
                }
            }
        }
        
    }
}
