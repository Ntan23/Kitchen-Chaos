using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI completeOrderDeliveredText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    GameManager gm;
    DeliveryManager deliveryManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        deliveryManager = DeliveryManager.Instance;

        gm.OnStateChanged += GameManager_OnStateChanged;
        
        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(!deliveryManager.win)
        {
            if(gm.IsGameOver()) 
            {
                gameObject.SetActive(true);
                completeOrderDeliveredText.text = deliveryManager.GetCompleteAmount().ToString() + " / " + deliveryManager.GetMaxCompleAmount().ToString();
            }
            else gameObject.SetActive(false);
        }
    }
}
