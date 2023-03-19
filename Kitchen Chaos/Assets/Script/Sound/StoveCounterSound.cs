using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    GameManager gm;
    bool playSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gm = GameManager.Instance;

        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        gm.OnGamePaused += GameManager_OnGamePaused;
        gm.OnGameUnpaused += GameManager_OnGameUnPaused;
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if(stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState()== StoveCounter.State.Fried) audioSource.Play();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        if(stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState() == StoveCounter.State.Fried) audioSource.Pause();
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State state)
    {
        if(state == StoveCounter.State.Frying || state == StoveCounter.State.Fried) playSound = true;
        else playSound = false;

        if(playSound) audioSource.Play();
        else audioSource.Pause();
    }
}
