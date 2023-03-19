using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter , IHasProgress
{
    #region ForEvent
    public event EventHandler OnCutAction;
    public event IHasProgress.HasProgressCounterEvent OnProgressChanged;
    #endregion

    #region Variables
    [SerializeField] private CanBeSlicedKitchenObjectsSO[] canBeSlicedKitchenObjectsSO;
    AudioManager audioManager;

    private float sliceCount;
    private float progress;
    private bool isComplete;
    #endregion

    void Start()
    {
        audioManager = AudioManager.Instance;
    }
    
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
            }
            else if(!playerInteraction.HasKitchenObject()) Debug.Log("Player Not Carrying Anything");
        }
        else if(HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject())
            {
                if(playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO())) GetKitchenObject().DestroyKitchenObject();
                }
            }
            else if(!playerInteraction.HasKitchenObject()) 
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);
                
                if(!isComplete) 
                {
                    OnProgressChanged?.Invoke(0f);
                    isComplete = false;
                }
            }
        }
    }

    public override void InteractAlternate(PlayerInteraction playerInteraction)
    {
        if(HasKitchenObject() && CanBeSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO()))
        {
            sliceCount++;

            CanBeSlicedKitchenObjectsSO canBeSlicedKitchenObjectSO = GetCanBeSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

            progress = sliceCount/canBeSlicedKitchenObjectSO.maxSliceCount;
            OnProgressChanged?.Invoke(progress);
            OnCutAction?.Invoke(this, EventArgs.Empty);
            //SoundOnCutAction?.Invoke(this, EventArgs.Empty);
            audioManager.CuttingCounter_SoundOnCutAction();
            
            if(canBeSlicedKitchenObjectSO.maxSliceCount <= sliceCount)
            {
                KitchenObjectsSO kitchenObjectsSO = GetSlicedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

                GetKitchenObject().DestroyKitchenObject();

                KitchenObjects.SpawnKitchenObject(kitchenObjectsSO, this);
                isComplete = true;
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
