using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winning : MonoBehaviour
{
    public QuestGenerator player;
    public void OnEnable()
    {
        ExitCheck exit = FindObjectOfType<ExitCheck>();
        player = FindObjectOfType<CharacterController>().GetComponent<QuestGenerator>();
        exit.OnRightItem.AddListener(CheckIfWon);
        exit.OnItemRemoved.AddListener(CheckIfLost);
    }

    public void CheckIfWon()
    {
        if (PaperListMenu.Instance.TextOrderedList.TrueForAll(x => x.fontStyle == FontStyles.Strikethrough))
        {
            GameManager.Instance.UpdateState(GameManager.GameState.END);
            UIManager.Instance.endMenu.text.text = "You win!";
        }
    }
    public void CheckIfLost()
    {
        for (int i = 0; i < player.itemsToCollect.Count; i++)
        {
            if (ItemsDatabase.Instance.objectsInScene[player.itemsToCollect[i]] < 1)
            {
                GameManager.Instance.UpdateState(GameManager.GameState.END);
                UIManager.Instance.endMenu.text.text = "You Loose ;_;";

            }
        }
    }
    

}
