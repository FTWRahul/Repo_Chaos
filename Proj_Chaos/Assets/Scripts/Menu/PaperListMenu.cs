﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperListMenu : MonoBehaviour
{
    public Events.EventMoveList onMoveList;

    private Animation _paperListMenuAnimator;
    [SerializeField] private AnimationClip moveInAnimationClip;
    [SerializeField] private AnimationClip moveOutAnimationClip;

    private PlayerMover player;
    public GameObject textPrefab;
    public Transform spawnPosition;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerMover>();
        _paperListMenuAnimator = GetComponent<Animation>();
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        Init();
    }
    
    public void Init()
    {
        QuestGenerator questGen = player.GetComponent<QuestGenerator>();
        foreach (int itemID in questGen.itemsToCollect)
        {
            Text nameText = Instantiate(textPrefab, spawnPosition.transform).GetComponent<Text>();
            nameText.text = ItemsDatabase.Instance.database[itemID].itemName;
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
    
    

/*    public void OnMoveOutComplete()
    {
        onMoveList.Invoke(false);
    }

    public void OnMoveInStart()
    {
        onMoveList.Invoke(true);
    }*/
    
    public void MoveIn()
    {
        //call move in on start
        _paperListMenuAnimator.Stop();
        _paperListMenuAnimator.clip = moveInAnimationClip;
        _paperListMenuAnimator.Play();
    }
    
    public void MoveOut()
    {
        _paperListMenuAnimator.Stop();
        _paperListMenuAnimator.clip = moveOutAnimationClip;
        _paperListMenuAnimator.Play();
    }

}
