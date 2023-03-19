using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.DifficultySelector);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        settingsButton.onClick.AddListener(() => {
            settingsUI.SetActive(true);
        });
    }
}
