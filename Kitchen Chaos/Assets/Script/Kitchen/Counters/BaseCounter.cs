using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    #region ForEvent
    public static event EventHandler SoundOnDropSomething;
    #endregion

    #region Variables
    [SerializeField] Transform counterTopPoint;
    private KitchenObjects kitchenObject;
    #endregion

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        
    }

    public virtual void InteractAlternate(PlayerInteraction playerInteraction)
    {
        
    }

    public Transform GetKitchenObjectParentTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null) SoundOnDropSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
