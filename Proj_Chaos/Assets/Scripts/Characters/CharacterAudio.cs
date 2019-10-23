﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour
{

    [SerializeField] private AudioClip[] slapClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip dropClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        // subscribe to animation controller event
    }

    public void PlaySlap()
    {
        _audioSource.Stop();
        _audioSource.clip = slapClip[Random.Range(0, slapClip.Length)];
        _audioSource.Play();
    }

    public void PlayPickUp()
    {
        _audioSource.Stop();
        _audioSource.clip = pickupClip;
        _audioSource.Play();
    }

    public void PlayDrop()
    {
        _audioSource.Stop();
        _audioSource.clip = dropClip;
        _audioSource.Play();
    }
}