using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    #region Singleton
    public static DeliveryCounter Instance {get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    DeliveryManager deliveryManager;

    void Start()
    {
        deliveryManager = DeliveryManager.Instance;
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(playerInteraction.HasKitchenObject()) 
        {
            if(playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                deliveryManager.DeliverRecipe(plateKitchenObject);

                playerInteraction.GetKitchenObject().DestroyKitchenObject();
            }
        }
    }
}
