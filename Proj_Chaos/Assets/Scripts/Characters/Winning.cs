using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winning : MonoBehaviour
{
    public void OnEnable()
    {
        FindObjectOfType<ExitCheck>().OnItemRemoved.AddListener(CheckIfWon);
    }

    public void CheckIfWon()
    {
        if (PaperListMenu.Instance.TextOrderedList.TrueForAll(x => x.fontStyle == FontStyles.Strikethrough))
        {
            GameManager.Instance.UpdateState(GameManager.GameState.END);
            UIManager.Instance.endMenu.text.text = "You win!";
        }
    }
}
