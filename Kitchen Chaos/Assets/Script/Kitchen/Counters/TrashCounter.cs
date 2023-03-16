using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(playerInteraction.HasKitchenObject()) playerInteraction.GetKitchenObject().DestroyKitchenObject();
        else if(!playerInteraction.HasKitchenObject()) Debug.Log("There is nothing in your hand");
    }
}
