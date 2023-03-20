using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    #region Singleton & SetDifficulty
    public static DeliveryManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;

        difficultyIndex = PlayerPrefs.GetInt("Difficulty");

        if(difficultyIndex == 1) 
        {
            maxWaitingRecipe = 2;
            maxCompleteOrder = 4;
            maxSpawnRecipeTimer = 6.0f;
        }
        if(difficultyIndex == 2) 
        {
            maxWaitingRecipe = 3;
            maxCompleteOrder = 7;
            maxSpawnRecipeTimer = 5.0f;
        }
        if(difficultyIndex == 3)
        {
            maxWaitingRecipe = 2;
            maxCompleteOrder = 5;
            maxSpawnRecipeTimer = 4.0f;
        } 
    }
    #endregion

    #region ForEvent
    public event EventHandler OnOrderSpawned;
    public event EventHandler OnOrderSent;
    #endregion

    #region FloatVariables
    private float spawnRecipeTimer = 0.5f;
    private float maxSpawnRecipeTimer;
    #endregion

    #region IntegerVariables
    private int maxWaitingRecipe;
    private int completedOrder;
    private int maxCompleteOrder;
    private int difficultyIndex;
    #endregion

    #region BoolVariables
    bool ingredientFound;
    bool plateIngredientsMatchesRecipe;
    [HideInInspector] public bool win;
    #endregion

    #region OtherVariables
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();
    GameManager gm;
    AudioManager audioManager;
    #endregion

    void Start()
    {
        gm = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    void Update()
    {
        if(gm.IsGamePlaying())
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
        else return;
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
                    audioManager.DeliveryManager_SoundOnOrderComplete();

                    if(completedOrder >= maxCompleteOrder)
                    {
                        win = true;
                    }
                    return;
                }
            }
        }

        //No Match Found
        audioManager.DeliveryManager_SoundOnOrderFailed();
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetCompleteAmount()
    {
        return completedOrder;
    }

    public int GetMaxCompleAmount()
    {
        return maxCompleteOrder;
    }
}
