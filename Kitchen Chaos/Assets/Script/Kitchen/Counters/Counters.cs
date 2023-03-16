using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounters : MonoBehaviour , IKitchenObjectParent
{
    [SerializeField] Transform counterTopPoint;
    private KitchenObjects kitchenObject;

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        Debug.LogError("BaseCounter Interacted");
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

    // void Interact(Transform prefab, Transform counterTopPoint);
}

public class ClearCounterType : BaseCounters
{
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        
    }
}


public class ContainerCounterType : BaseCounters
{
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(playerInteraction);

        
    }
}

// public abstract class Counter
// {
//     public virtual void Interact(Transform prefab, Transform counterTopPoint)
//     {
        
//     }
// }

// public class ClearCounter : IClearCounter
// {
//     public void Interact(Transform prefab, Transform counterTopPoint)
//     {
//         Debug.Log("Clear Counter Interacted");
//         Transform prefabTransform = Object.Instantiate(prefab,counterTopPoint);
//         prefabTransform.localPosition = Vector3.zero;
//     }
// }

// public class NotClearCounter : Counter
// {
//     public override void Interact(Transform prefab, Transform counterTopPoint)
//     {
//         Debug.Log("Not Clear Counter Interacted");
//     }
// }

public enum counters 
{
    ClearCounter , ContainerCounter
}

public class Counters : MonoBehaviour
{
    public counters counterType;
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] PlayerInteraction playerInteraction;

    #region ForEvent
    public event ContainerAnimation OnPlayerGrabObject;
    public delegate void ContainerAnimation();
    #endregion

    // public void InteractCounter(Counter counter, Transform prefab, Transform counterTopPoint)
    // {
    //     counter.Interact(prefab, counterTopPoint);
    // }

    public void CheckWhichCounterIsInteracted()
    {
        switch(counterType)
        {
            case counters.ClearCounter :
                // IClearCounter clearCounterInterface = new ClearCounter();
                GetComponent<ClearCounterType>().Interact(playerInteraction);
                // clearCounterInterface.Interact(kitchenObjectsSO.prefab, counterTopPoint);
                // ClearCounter clearCounter =  new ClearCounter();
                // InteractCounter(clearCounter, prefab, counterTopPoint);
                break;
            case counters.ContainerCounter :
                GetComponent<ContainerCounter>().Interact(playerInteraction);
                OnPlayerGrabObject?.Invoke();
                // NotClearCounter notClearCounter =  new NotClearCounter();
                // InteractCounter(notClearCounter,null,null);
                break;
        }
    }
}


