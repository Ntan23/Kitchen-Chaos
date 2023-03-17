using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI completeOrderDeliveredText;
    GameManager gm;
    DeliveryManager deliveryManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        deliveryManager = DeliveryManager.Instance;

        gm.OnStateChanged += GameManager_OnStateChanged;
        
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(gm.IsGameOver()) 
        {
            gameObject.SetActive(true);
            completeOrderDeliveredText.text = deliveryManager.GetCompleteAmount().ToString();
        }
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
