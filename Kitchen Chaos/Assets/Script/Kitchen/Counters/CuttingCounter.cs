using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    #region ForEvent
    public delegate void CuttingCounterEvent(int sliceCount, int maxSliceCount);
    public event CuttingCounterEvent OnCutProgressChanged;
    public event EventHandler OnCutAction;
    #endregion

    [SerializeField] private CanBeSlicedKitchenObjectsSO[] canBeSlicedKitchenObjectsSO;

    private int sliceCount;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(!HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) 
            {
                if(CanBeSlicedKitchenObject(playerInteraction.GetKitchenObject().GetKitchenObjectsSO()))
                {
                    playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);

                    sliceCount = 0;
                }
                // else if(!CanBeSlicedKitchenObject(playerInteraction.GetKitchenObject().GetKitchenObjectsSO())) Debug.Log("That Cannot Be Cut");
            }
            else if(!playerInteraction.HasKitchenObject()) Debug.Log("Player Not Carrying Anything");
        }
        else if(HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) Debug.Log("Player Carrying Something");
            else if(!playerInteraction.HasKitchenObject()) GetKitchenObject().SetKitchenObjectParent(playerInteraction);
        }
    }

    public override void InteractAlternate(PlayerInteraction playerInteraction)
    {
        if(HasKitchenObject() && CanBeSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO()))
        {
            sliceCount++;

            CanBeSlicedKitchenObjectsSO canBeSlicedKitchenObjectSO = GetCanBeSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

            OnCutProgressChanged?.Invoke(sliceCount, canBeSlicedKitchenObjectSO.maxSliceCount);
            OnCutAction?.Invoke(this, EventArgs.Empty);
            
            if(canBeSlicedKitchenObjectSO.maxSliceCount <= sliceCount)
            {
                 KitchenObjectsSO kitchenObjectsSO = GetSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

                GetKitchenObject().DestroyKitchenObject();

                KitchenObjects.SpawnKitchenObject(kitchenObjectsSO, this);
            }
        }
    }

    private CanBeSlicedKitchenObjectsSO GetCanBeSlicedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach(CanBeSlicedKitchenObjectsSO canBeSliced in canBeSlicedKitchenObjectsSO)
        {
            if(canBeSliced.unsliced == kitchenObjectsSO) return canBeSliced;
        }

        return null;
    }

    private KitchenObjectsSO GetSlicedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        CanBeSlicedKitchenObjectsSO canBeSlicedKitchenObjectSO = GetCanBeSlicedKitchenObject(kitchenObjectsSO);

        if(canBeSlicedKitchenObjectSO != null) return canBeSlicedKitchenObjectSO.sliced;
        else return null;
    }

    private bool CanBeSlicedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        CanBeSlicedKitchenObjectsSO canBeSlicedKitchenObjectsSO = GetCanBeSlicedKitchenObject(kitchenObjectsSO);

        return canBeSlicedKitchenObjectsSO != null;
    }
}
