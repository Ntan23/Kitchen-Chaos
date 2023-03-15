using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour , IKitchenObjectParent
{
    #region Variables
    [SerializeField] private ClearCounter selectedCounter;
    [SerializeField] private GameObject selectedVisual;
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] Transform counterTopPoint;
    private KitchenObjects kitchenObject;
    #endregion

    void Start()
    {
        PlayerInteraction.Instance.OnSelectedCounterChanged += Interaction_OnSelectedCounterChanged;
    }

    private void Interaction_OnSelectedCounterChanged(object sender, PlayerInteraction.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == selectedCounter)
        {
            selectedVisual.SetActive(true);
        }
        else if(e.selectedCounter != selectedCounter)
        {
            selectedVisual.SetActive(false);
        }
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        if(kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab,counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(this);
            // kitchenObjectTransform.localPosition = Vector3.zero;
        }
        else if(kitchenObject != null)
        {
            //Give The Kitchen Object To Player
            kitchenObject.SetKitchenObjectParent(playerInteraction);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
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


