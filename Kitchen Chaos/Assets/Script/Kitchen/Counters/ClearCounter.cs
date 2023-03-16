using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(!HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);
            else if(!playerInteraction.HasKitchenObject()) Debug.Log("Player Not Carrying Anything");
        }
        else if(HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) Debug.Log("Player Carrying Something");
            else if(!playerInteraction.HasKitchenObject()) GetKitchenObject().SetKitchenObjectParent(playerInteraction);
        }
    }
}


