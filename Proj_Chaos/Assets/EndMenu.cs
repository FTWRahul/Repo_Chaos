using System;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;

    private void OnEnable()
    {
        EnablePanel();
    }

    public void EnablePanel()
    {
        switch (UIManager.Instance.currentEndType)
        {
            case EndType.WIN:
                failPanel.SetActive(false);
                winPanel.SetActive(true);
                break;
            
            case EndType.FAIL:
                winPanel.SetActive(false);
                failPanel.SetActive(true);
                break;

            case EndType.NONE:
                failPanel.SetActive(false);
                winPanel.SetActive(false);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
