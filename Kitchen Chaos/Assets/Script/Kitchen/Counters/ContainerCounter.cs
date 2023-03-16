using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    #region ForEvent
    public event EventHandler OnPlayerGrabObject;
    #endregion
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(!playerInteraction.HasKitchenObject())
        {
            KitchenObjects.SpawnKitchenObject(kitchenObjectsSO, playerInteraction);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
        else if(playerInteraction.HasKitchenObject()) Debug.Log("Player Already Carrying Something");
    }
}
