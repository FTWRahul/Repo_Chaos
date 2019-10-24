﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum UIAnimationType
{
    MOVE,
    SCALE,
    SCALEX,
    SCALEY,
    FADE
}
public class UITweener : MonoBehaviour
{
    public UIAnimationType animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;

    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;

    public bool showOnEnable;
    public bool workOnDisable;
    
    private GameObject _objectToAnimate;
    private LTDescr _tweenObject;

    private void OnEnable()
    {
        if(!showOnEnable) return;

        Show();
    }

    private void Show()
    {
        HandleTween();
    }

    private void HandleTween()
    {
        if (_objectToAnimate == null)
        {
            _objectToAnimate = gameObject;
        }

        switch (animationType)
        {
            case UIAnimationType.MOVE:
                MoveAbsolute();
                break;
            
            case UIAnimationType.SCALE:
                Scale();
                break;
            
            case UIAnimationType.SCALEX:
                Scale();
                break;
            
            case UIAnimationType.SCALEY:
                Scale();
                break;
            
            case UIAnimationType.FADE:
                Fade();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        _tweenObject.setDelay(delay);
        _tweenObject.setEase(easeType);

        if (loop)
        {
            _tweenObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            _tweenObject.setLoopPingPong();
        }
    }

    private void Scale()
    {
        throw new NotImplementedException();
    }

    private void Fade()
    {
        if (!gameObject.GetComponent<CanvasGroup>())
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        if (startPositionOffset)
        {
            _objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
        }

        _tweenObject = LeanTween.alphaCanvas(_objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
    }

    private void MoveAbsolute()
    {
        _objectToAnimate.GetComponent<RectTransform>().anchoredPosition = from;
        _tweenObject = LeanTween.move(_objectToAnimate.GetComponent<RectTransform>(), to, duration);
    }
}