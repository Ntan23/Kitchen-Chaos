using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] Transform counterTopPoint;
    private KitchenObjects kitchenObject;

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        Debug.LogError("BaseCounter Interacted");
    }

    public virtual void InteractAlternate(PlayerInteraction playerInteraction)
    {
        //Debug.LogError("BaseCounter Interacted");
    }

    public Transform GetKitchenObjectParentTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
