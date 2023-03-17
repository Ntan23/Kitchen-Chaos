using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAddedIngredientVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectsSO_GO
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject GO;
    }

    [SerializeField] PlateKitchenObject platesKitchenObject;
    [SerializeField] private List<KitchenObjectsSO_GO> kitchenObjectsSO_GOList = new List<KitchenObjectsSO_GO>();
    
    // Start is called before the first frame update
    void Start()
    {
        platesKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach(KitchenObjectsSO_GO kitchenObjectsSO_GO in kitchenObjectsSO_GOList)
        {
            kitchenObjectsSO_GO.GO.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach(KitchenObjectsSO_GO kitchenObjectsSO_GO in kitchenObjectsSO_GOList)
        {
            if(kitchenObjectsSO_GO.kitchenObjectsSO == kitchenObjectsSO) kitchenObjectsSO_GO.GO.SetActive(true);
        }
    }
}
