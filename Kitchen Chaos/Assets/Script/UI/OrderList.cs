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

            StartCoroutine(AnimateSpawn(recipeTransform.gameObject));

            recipeTransform.GetComponent<OrderListSingleUI>().SetRecipeSO(recipeSO);
        }   
    }

    IEnumerator AnimateSpawn(GameObject gameObject)
    {
        LeanTween.scale(gameObject, new Vector3(1.1f,1.1f,1.1f), 0.3f);
        yield return new WaitForSeconds(0.2f);
        LeanTween.scale(gameObject, new Vector3(1.0f,1.0f,1.0f), 0.3f);
    }
}
