using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    #region ForEvent
    public event EventHandler OnStateChanged;
    #endregion

    #region Variables
    private enum State {
        WaitToStart, CountdownToStart, GameIsPlaying, GameOver
    }
    
    private State state;
    private float WaitToStartTimer = 0.5f;
    private float countdownToStartTimer = 3.0f;
    private float gameplayTimer;
    private float maxGameplayTimer = 10.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = State.WaitToStart;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.WaitToStart :
                WaitToStartTimer -= Time.deltaTime;

                if(WaitToStartTimer < 0f) 
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart :
                countdownToStartTimer -= Time.deltaTime;

                if(countdownToStartTimer < 0f) 
                {
                    state = State.GameIsPlaying;

                    gameplayTimer = 0.0f;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameIsPlaying :
                gameplayTimer += Time.deltaTime;

                if(gameplayTimer > maxGameplayTimer) 
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver :
                break;
        }

        //Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GameIsPlaying;
    }

    public bool IsCountdownStarted()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTime()
    {
        return countdownToStartTimer;
    }

    public float GetGameplayTimerNormalized()
    {
        return 1 - (gameplayTimer/maxGameplayTimer);
    }

}
