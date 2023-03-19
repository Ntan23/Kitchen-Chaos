using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OrderDeliveredIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orderDeliveredIndicatorText;
    DeliveryManager deliveryManager;

    // Start is called before the first frame update
    void Start()
    {
        deliveryManager = DeliveryManager.Instance;

        deliveryManager.OnOrderSent += DeliveryManager_OnOrderSent;

        UpdateText();
    }

    private void DeliveryManager_OnOrderSent(object sender, EventArgs e)
    {
        UpdateText();
    }

    void UpdateText()
    {
        orderDeliveredIndicatorText.text = "Order Delivered : " + deliveryManager.GetCompleteAmount().ToString() + " / " + deliveryManager.GetMaxCompleAmount().ToString();
    }
}
