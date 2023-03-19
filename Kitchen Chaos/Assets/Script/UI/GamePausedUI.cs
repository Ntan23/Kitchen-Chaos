using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingsUI;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        gm.OnGamePaused += GameManager_OnGamePaused;
        gm.OnGameUnpaused += GameManager_OnGameUnpaused;

        resumeButton.onClick.AddListener(() => {
            gm.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        settingButton.onClick.AddListener(() => {
            settingsUI.SetActive(true);
        });

        gameObject.SetActive(false);
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
