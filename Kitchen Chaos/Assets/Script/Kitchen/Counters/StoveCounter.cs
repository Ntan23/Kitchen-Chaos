using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    #region ForEvent 
    public delegate void StoveCounterEvent(State state);
    public event StoveCounterEvent OnStateChanged;
    #endregion

    public enum State {
        Idle,Frying,Fried,Burned
    }

    private State state;

    [SerializeField] private CanBeCookedKitchenObjectsSO[] canBeCookedKitchenObjectsSO;
    [SerializeField] private CanBeBurnedKitchenObjectsSO[] canBeBurnedKitchenObjectsSO;

    private float cookTimer;
    private float burnTimer;
    CanBeCookedKitchenObjectsSO canBeCookedKitchenObjectSO;
    CanBeBurnedKitchenObjectsSO canBeBurnedKitchenObjectSO;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        if(HasKitchenObject())
        {
            switch(state)
            {
                case State.Idle:
                    break;
                case State.Frying :
                    cookTimer += Time.deltaTime;

                    if(cookTimer > canBeCookedKitchenObjectSO.maxTimeToCook)
                    {
                        GetKitchenObject().DestroyKitchenObject();

                        KitchenObjects.SpawnKitchenObject(canBeCookedKitchenObjectSO.cooked, this);

                        state = State.Fried;
                        burnTimer = 0f;
                        canBeBurnedKitchenObjectSO = GetCanBeBurnedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

                        OnStateChanged?.Invoke(state);
                    }
                    break;
                case State.Fried :
                    burnTimer += Time.deltaTime;

                    if(burnTimer > canBeBurnedKitchenObjectSO.maxTimeToBurn)
                    {
                        GetKitchenObject().DestroyKitchenObject();

                        KitchenObjects.SpawnKitchenObject(canBeBurnedKitchenObjectSO.burned, this);

                        state = State.Burned;
                        OnStateChanged?.Invoke(state);
                    }
                    break;
                case State.Burned :
                    break; 
            }
        }
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(!HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) 
            {
                if(CanBeCookedKitchenObject(playerInteraction.GetKitchenObject().GetKitchenObjectsSO()))
                {
                    playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);

                    canBeCookedKitchenObjectSO = GetCanBeCookedKitchenObject(GetKitchenObject().GetKitchenObjectsSO());

                    state = State.Frying;
                    cookTimer = 0f;
                    OnStateChanged?.Invoke(state);
                }
            }
            else if(!playerInteraction.HasKitchenObject()) Debug.Log("Player Not Carrying Anything");
        }
        else if(HasKitchenObject())
        {
            if(playerInteraction.HasKitchenObject()) Debug.Log("Player Carrying Something");
            else if(!playerInteraction.HasKitchenObject()) 
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);

                state = State.Idle;
                OnStateChanged?.Invoke(state);
            }
        }
    }

    private CanBeCookedKitchenObjectsSO GetCanBeCookedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach(CanBeCookedKitchenObjectsSO canBeCooked in canBeCookedKitchenObjectsSO)
        {
            if(canBeCooked.uncooked == kitchenObjectsSO) return canBeCooked;
        }

        return null;
    }

    private CanBeBurnedKitchenObjectsSO GetCanBeBurnedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach(CanBeBurnedKitchenObjectsSO canBeBurned in canBeBurnedKitchenObjectsSO)
        {
            if(canBeBurned.cooked == kitchenObjectsSO) return canBeBurned;
        }

        return null;
    }

    private KitchenObjectsSO GetCookedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        CanBeCookedKitchenObjectsSO canBeCookedKitchenObjectSO = GetCanBeCookedKitchenObject(kitchenObjectsSO);

        if(canBeCookedKitchenObjectSO != null) return canBeCookedKitchenObjectSO.cooked;
        else return null;
    }

    private bool CanBeCookedKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        CanBeCookedKitchenObjectsSO canBeCookedKitchenObjectSO = GetCanBeCookedKitchenObject(kitchenObjectsSO);

        return canBeCookedKitchenObjectSO != null;
    }

    
}
