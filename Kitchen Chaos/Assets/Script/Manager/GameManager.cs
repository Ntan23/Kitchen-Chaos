using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton & Set Difficulty
    public static GameManager Instance;

    void Awake()
    {
        if(Instance == null) Instance = this;

        difficultyIndex = PlayerPrefs.GetInt("Difficulty");

        if(difficultyIndex == 1) maxGameplayTimer = 240.0f;
        if(difficultyIndex == 2) maxGameplayTimer = 120.0f;
        if(difficultyIndex == 3) maxGameplayTimer = 60.0f;
    }
    #endregion

    #region ForEvent
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    #endregion

    #region Variables
    #region EnumVariables
    private enum State {
        WaitToStart, CountdownToStart, GameIsPlaying, GameOver
    }
    
    private State state;
    #endregion

    #region FloatVariables
    private float WaitToStartTimer = 0.5f;
    private float countdownToStartTimer = 3.0f;
    private float gameplayTimer;
    private float maxGameplayTimer;
    #endregion
    
    #region BoolVariables
    private bool isGamePaused = false;
    #endregion

    #region IntegerVariables
    private int difficultyIndex;
    #endregion

    #region OtherVariables
    private GameInputManager gameInputManager;
    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameInputManager = GameInputManager.Instance;
        state = State.WaitToStart;

        gameInputManager.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
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

                if(DeliveryManager.Instance.win) 
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver :
                break;
        }
    }

    public bool IsCountdownStarted()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGamePlaying()
    {
        return state == State.GameIsPlaying;
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

    public void TogglePauseGame()
    {
        if(isGamePaused) 
        {
            Time.timeScale = 1.0f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        else if(!isGamePaused) 
        {
            Time.timeScale = 0.0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }

        isGamePaused = !isGamePaused;
    }
}
