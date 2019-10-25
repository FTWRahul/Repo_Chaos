using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextFlicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _text;
    private float _addAmount = 0.035f;
    private float _upperLimit = 1f;
    private float _lowerLimit = 0f;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Flick());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        SetDefault();
    }

    public void SetDefault()
    {
        _text.color = Color.white;
    }

    private IEnumerator Flick()
    {
        while (true)
        {
            if (_text.color.a + _addAmount >= _upperLimit || _text.color.a + _addAmount <= _lowerLimit)
            {
                
                _addAmount *= -1;
            }
            
            _text.color = new Color(_text.color.r,_text.color.b,_text.color.g,_text.color.a + _addAmount);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
