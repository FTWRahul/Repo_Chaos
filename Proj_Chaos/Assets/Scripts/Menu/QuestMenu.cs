using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : Singleton<QuestMenu>
{
    public Events.EventMoveList onMoveList;
    
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
        if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.OPENLIST)
        {
            MoveIn();
        }

        if (previousState == GameManager.GameState.OPENLIST && currentState == GameManager.GameState.RUNNING)
        {
            MoveOut();
        }
    }

    public void MoveIn()
    {
       _uiTweener.HandleTween();
       _uiTweener.SwapDirection();
    }
    
    public void MoveOut()
    {
        _uiTweener.HandleTween();
        _uiTweener.SwapDirection();
    }

    public void StrikeItem(int id)
    {
        if (textOrderedList[id].fontStyle != FontStyles.Strikethrough)
        {
            textOrderedList[id].fontStyle = FontStyles.Strikethrough;
        }
    }
}
