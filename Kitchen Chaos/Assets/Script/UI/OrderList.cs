using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    DeliveryManager deliveryManager;

    void Start()
    {
        deliveryManager = DeliveryManager.Instance;

        deliveryManager.OnOrderSpawned += DeliveryManager_OnOrderSpawned;
        deliveryManager.OnOrderSent += DeliveryManager_OnOrderSent;
    }

    private void DeliveryManager_OnOrderSpawned(object sender, EventArgs e)
    {
        UpdateOrderList();
    }

    private void DeliveryManager_OnOrderSent(object sender, EventArgs e)
    {
        UpdateOrderList();
    }

    public void UpdateOrderList()
    {
        foreach(Transform child in container)
        {
            if(child == recipeTemplate) continue;
            else Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in deliveryManager.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.GetComponent<OrderListSingleUI>().SetRecipeSO(recipeSO);
        }   
    }
}
