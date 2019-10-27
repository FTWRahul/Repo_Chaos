using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EventType
{
    FAIL,
    WIN,
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource menuMusicSource;
    [SerializeField] private AudioSource inGameMusicSource;
    [SerializeField] private AudioSource crowdSource;
    [SerializeField] private AudioSource eventEffectsSource;

    [SerializeField] public AudioClip menuClip;
    [SerializeField] public AudioClip elevatorInGameClip;
    [SerializeField] public AudioClip[] crowdClips;
    [SerializeField] public AudioClip failGameClip;
    [SerializeField] public AudioClip winGameClip;
    [SerializeField] public AudioClip doorClip;
    [SerializeField] public AudioClip paperClip;

    private CharacterController _characterController;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        crowdSource.clip = crowdClips[Random.Range(0, crowdClips.Length)];
        menuMusicSource.clip = menuClip;
        inGameMusicSource.clip = elevatorInGameClip;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void PlayQuestMenuClip()
    {
        eventEffectsSource.Stop();
        eventEffectsSource.clip = paperClip;
        eventEffectsSource.Play();
    }

    private void HandleGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        switch (currentState)
        {
            case GameManager.GameState.RUNNING:
            {
                if (previousState == GameManager.GameState.PREGAME || previousState == GameManager.GameState.MENU)
                {
                    StartCoroutine(MenuToGameTransition());
                }
                if (previousState == GameManager.GameState.PAUSED)
                {
                    crowdSource.Play();
                }
                break;
            }

            case GameManager.GameState.PAUSED:
                crowdSource.Pause();
                break;
            
            case GameManager.GameState.END:
                crowdSource.Stop();
                inGameMusicSource.Stop();
                break;
            
            case GameManager.GameState.MENU:
                menuMusicSource.Stop();
                menuMusicSource.clip = menuClip;
                menuMusicSource.Play();
                break;
            
            case GameManager.GameState.PREGAME:
                break;
            
            case GameManager.GameState.QUEST:
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
        }
    }

    private IEnumerator MenuToGameTransition()
    {
        inGameMusicSource.Play();
        inGameMusicSource.volume = 0;

        while (inGameMusicSource.volume < 1)
        {
            inGameMusicSource.volume += 0.01f;
            menuMusicSource.volume -= 0.01f;

            yield return new WaitForEndOfFrame();
        }
        
        menuMusicSource.Stop();
        crowdSource.Play();

        yield return null;
    }
    public void PlayEvent(EventType type)
    {
        switch (type)
        {
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

            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
