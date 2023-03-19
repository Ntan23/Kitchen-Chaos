using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(playerInteraction.HasKitchenObject()) 
        {
            playerInteraction.GetKitchenObject().DestroyKitchenObject();
            audioManager.TrashCounter_SoundOnTrashSomething();
        }
    }
}
