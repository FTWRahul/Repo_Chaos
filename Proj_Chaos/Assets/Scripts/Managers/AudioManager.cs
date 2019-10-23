using System;
using UnityEngine;

public enum EventType
{
    MONEY,
    WRONG,
    FAIL,
    WIN,
    GARAGE,
    PAPER
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource crowdSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource eventEffectsSource;

    [SerializeField] public AudioClip menuClip;
    [SerializeField] public AudioClip elevatorDefaultClip;
    [SerializeField] public AudioClip elevatorInGameClip;
    [SerializeField] public AudioClip[] crowdClips;
    [SerializeField] public AudioClip moneyClip;
    [SerializeField] public AudioClip wrongItemClip;
    [SerializeField] public AudioClip failGameClip;
    [SerializeField] public AudioClip winGameClip;
    [SerializeField] public AudioClip doorClip;
    [SerializeField] public AudioClip paperClip;

    private CharacterController _characterController;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        crowdSource.clip = crowdClips[0];
        musicSource.clip = menuClip;
    }

    public void InitPlayer()
    {
        _characterController = FindObjectOfType<CharacterController>();
        _characterController.GetComponent<ExitCheck>().OnRightItem.AddListener(RightItem);
        _characterController.GetComponent<ExitCheck>().OnWrongItem.AddListener(WrongItem);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged.RemoveListener(HandleGameStateChanged);
        if (_characterController != null)
        {
            _characterController.GetComponent<ExitCheck>().OnRightItem.AddListener(RightItem);
            _characterController.GetComponent<ExitCheck>().OnWrongItem.AddListener(WrongItem);
        }
    }

    private void RightItem()
    {
        PlayEvent(EventType.MONEY);
    }

    public void WrongItem()
    {
        PlayEvent(EventType.WRONG);
    }


    private void HandleGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {

        if (currentState == GameManager.GameState.RUNNING)
        {
            crowdSource.Play();

            if (musicSource.clip != elevatorInGameClip)
            {
                musicSource.Stop();
                musicSource.clip = elevatorInGameClip;
                musicSource.Play();
            }
                
            
            if (previousState == GameManager.GameState.PREGAME || previousState == GameManager.GameState.MENU)
            {
                musicSource.Stop();
                musicSource.Play();
            }
        }
        else if (currentState == GameManager.GameState.PAUSED)
        {
            crowdSource.Stop();
        }
        else if (currentState == GameManager.GameState.END)
        {
            crowdSource.Stop();
            musicSource.Stop();
        }
        else if (currentState == GameManager.GameState.PREGAME)
        {
            crowdSource.Stop();
            
            musicSource.Stop();
            musicSource.clip = menuClip;
            musicSource.Play();
        }
    }

    public void PlayEvent(EventType type)
    {
        switch (type)
        {
            case EventType.MONEY:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = moneyClip;
                eventEffectsSource.Play();
                break;
            
            case EventType.WRONG:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = wrongItemClip;
                eventEffectsSource.Play();
                break;
            
            case EventType.FAIL:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = failGameClip;
                eventEffectsSource.Play();
                break;
            
            case EventType.WIN:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = winGameClip;
                eventEffectsSource.Play();
                break;
            
            case EventType.GARAGE:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = doorClip;
                eventEffectsSource.Play();
                break;
            
            case EventType.PAPER:
                eventEffectsSource.Stop();
                eventEffectsSource.clip = paperClip;
                eventEffectsSource.Play();
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
