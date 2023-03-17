using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    #region ForEvent
    public delegate void AddIngredientEvent(KitchenObjectsSO kitchenObjectsSO);
    public event AddIngredientEvent OnIngredientAdded;
    #endregion
    
    #region Variables
    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectsSOList;
    private List<KitchenObjectsSO> kitchenObjectsSOList = new List<KitchenObjectsSO>();
    #endregion
    
    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        if(!validKitchenObjectsSOList.Contains(kitchenObjectsSO)) return false;

        if(!kitchenObjectsSOList.Contains(kitchenObjectsSO)) 
        {
            kitchenObjectsSOList.Add(kitchenObjectsSO);
            OnIngredientAdded?.Invoke(kitchenObjectsSO);
            return true;
        }
        else return false;
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList()
    {
        return kitchenObjectsSOList;
    }
}
