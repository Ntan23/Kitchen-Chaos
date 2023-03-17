using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    #region ForEvent
    public static event EventHandler SoundOnTrashSomething;
    #endregion

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(playerInteraction.HasKitchenObject()) 
        {
            playerInteraction.GetKitchenObject().DestroyKitchenObject();
            SoundOnTrashSomething?.Invoke(this, EventArgs.Empty);
        }
    }
}
