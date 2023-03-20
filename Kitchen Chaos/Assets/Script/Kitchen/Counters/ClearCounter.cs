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
        }
        else if(HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject())
            {
                //Check If There Is A Plate In Player's Hand
                if(playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())) GetKitchenObject().DestroyKitchenObject();
                }
                else
                {
                    //Check If There Is A Plate In Counter
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(playerInteraction.GetKitchenObject().GetKitchenObjectsSO())) playerInteraction.GetKitchenObject().DestroyKitchenObject();
                    }
                }
            }
            else if(!playerInteraction.HasKitchenObject()) GetKitchenObject().SetKitchenObjectParent(playerInteraction);
        }
    }
}


