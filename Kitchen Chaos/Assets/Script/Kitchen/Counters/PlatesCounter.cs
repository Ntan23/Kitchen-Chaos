using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    #region ForEvent
    public event EventHandler OnPlateSpawnned;
    public event EventHandler OnPlateTaken;
    #endregion

    #region FloatVariables
    private float spawnPlateTimer = 3.5f;
    private float maxSpawnPlateTimer = 4.0f;
    #endregion

    #region IntegerVariables
    private int platesSpawnedAmount;
    private int maxPlatesSpawnedAmount = 4;
    #endregion

    #region OtherVariables
    [SerializeField] KitchenObjectsSO plateSO;
    GameManager gm;
    #endregion

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if(gm.IsGamePlaying())
        {
            spawnPlateTimer += Time.deltaTime;

            if(spawnPlateTimer > maxSpawnPlateTimer)
            {
                spawnPlateTimer = 0.0f;

                if(maxPlatesSpawnedAmount > platesSpawnedAmount)
                {
                    platesSpawnedAmount++;

                    OnPlateSpawnned?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        else return;
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(!playerInteraction.HasKitchenObject()) 
        {
            if(platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;

                KitchenObjects.SpawnKitchenObject(plateSO, playerInteraction);

                OnPlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
