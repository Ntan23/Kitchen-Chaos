using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIngredientIcons : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    // Start is called before the first frame update
    void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectsSO kitchenObjectsSO)
    {
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        foreach(Transform child in transform)
        {
            if(child == iconTemplate) continue;
            else Destroy(child.gameObject);
        }

        foreach(KitchenObjectsSO kitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
           
            iconTransform.GetComponent<PlateIngredientSingleIcon>().SetKitchenObjectSO(kitchenObjectsSO);
        }   
    }
}
