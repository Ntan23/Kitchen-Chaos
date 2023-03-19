using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectorUI : MonoBehaviour
{
    [SerializeField] private Button relaxedDifficultyButton;
    [SerializeField] private Button normalDifficultyButton;
    [SerializeField] private Button hardcoreDifficultyButton;

    // Start is called before the first frame update
    void Start()
    {
        relaxedDifficultyButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",1);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        normalDifficultyButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",2);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        hardcoreDifficultyButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",3);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
