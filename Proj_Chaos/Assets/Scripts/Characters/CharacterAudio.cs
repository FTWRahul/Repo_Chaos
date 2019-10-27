using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AudioEventType
{
    MONEY,
    WRONG,
    SLAP,
    PICKUP
}

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] slapClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioClip wrongItemClip;
    [SerializeField] private AudioClip moneyClip;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySlap()
    {
        PlayEvent(AudioEventType.SLAP);
    }

    public void PlayPickUp()
    {
        PlayEvent(AudioEventType.PICKUP);
    }

    public void PlayMoney()
    {
        PlayEvent(AudioEventType.MONEY);
    }

    public void PlayWrong()
    {
        PlayEvent(AudioEventType.WRONG);
    }
    
    private void PlayEvent(AudioEventType type)
    {
        _audioSource.Stop();

        switch (type)
        {
            case AudioEventType.MONEY:
                _audioSource.clip = moneyClip;
                break;
            
            case AudioEventType.WRONG:
                _audioSource.clip = wrongItemClip;
                break;
            
            case AudioEventType.SLAP:
                _audioSource.clip = slapClip[Random.Range(0, slapClip.Length)];
                break;
            
            case AudioEventType.PICKUP:
                _audioSource.clip = pickupClip;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        _audioSource.Play();
    }
}
