using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    bool playSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State state)
    {
        if(state == StoveCounter.State.Frying || state == StoveCounter.State.Fried) playSound = true;
        else playSound = false;

        if(playSound) audioSource.Play();
        else audioSource.Pause();
    }
}
