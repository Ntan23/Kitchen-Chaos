using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    bool playSound;
    bool playWarningSound;
    private float warningSoundTimer;
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    GameManager gm;
    AudioManager audioManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gm = GameManager.Instance;
        audioManager = AudioManager.Instance;

        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        gm.OnGamePaused += GameManager_OnGamePaused;
        gm.OnGameUnpaused += GameManager_OnGameUnPaused;
    }

    void Update()
    {
        if(playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;

            if(warningSoundTimer <= 0.0f)
            {
                warningSoundTimer = 0.2f;
                audioManager.PlayWarningSound();
            }
        }
    }

    private void StoveCounter_OnProgressChanged(float progressValue)
    {
        if(stoveCounter.GetState() == StoveCounter.State.Fried)
        {
            playWarningSound = progressValue >= 0.4f && progressValue <= 1.0f;
        }
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        if(gm.IsGamePlaying())
        if(stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState() == StoveCounter.State.Fried) audioSource.Play();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {   
        if(gm.IsGamePlaying())
        if(stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState() == StoveCounter.State.Fried) audioSource.Pause();
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State state)
    {   
        if(gm.IsGamePlaying())
        {
            if(state == StoveCounter.State.Frying || state == StoveCounter.State.Fried) playSound = true;
            else playSound = false;
        }
        else if(gm.IsGameOver()) playSound = false;

        if(playSound) audioSource.Play();
        else audioSource.Pause();
    }
}
