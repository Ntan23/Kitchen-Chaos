using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    #region Singleton
    public static DeliveryManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    #region ForEvent
    public event EventHandler OnOrderSpawned;
    public event EventHandler OnOrderSent;
    public event EventHandler SoundOnOrderComplete;
    public event EventHandler SoundOnOrderFailed;
    #endregion

    #region FloatVariables
    private float spawnRecipeTimer;
    private float maxSpawnRecipeTimer = 4.0f;
    #endregion

    #region IntegerVariables
    private int maxWaitingRecipe = 3;
    private int completedOrder;
    #endregion

    #region BoolVariables
    bool ingredientFound;
    bool plateIngredientsMatchesRecipe;
    #endregion

    #region OtherVariables
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();
    #endregion

    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = maxSpawnRecipeTimer;

            if(maxWaitingRecipe > waitingRecipeSOList.Count)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeList[UnityEngine.Random.Range(0,recipeListSO.recipeList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);
                OnOrderSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i=0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            //Has The Same Number Of Ingredients
            if(waitingRecipeSO.ingredients.Count == plateKitchenObject.GetKitchenObjectsSOList().Count)
            {
                plateIngredientsMatchesRecipe = true;
                //Check All Ingredients On The Recipe
                foreach(KitchenObjectsSO recipeIngredient in waitingRecipeSO.ingredients)
                {
                    ingredientFound = false;
                    //Check All Ingredients On The Plate
                    foreach(KitchenObjectsSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {
                        if(recipeIngredient == plateKitchenObjectSO)
                        {
                            //Ingredient Match;
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound)
                    {
                        //The ingredients was not founf on the plate
                        plateIngredientsMatchesRecipe = false;
                    }
                }

                if(plateIngredientsMatchesRecipe)
                {
                    waitingRecipeSOList.RemoveAt(i);
                    completedOrder++;
                    OnOrderSent?.Invoke(this, EventArgs.Empty);
                    SoundOnOrderComplete?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        //No Match Found
        SoundOnOrderFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetCompleteAmount()
    {
        return completedOrder;
    }
}
