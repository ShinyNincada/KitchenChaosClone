using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliverManager Instance { get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private List<RecipeSO> waitingRecipeSOList;
    private int maxMission = 4;
    public float spawnTimer = 4f;
    private float spawnTimerCD = 4f;
    public DeliveryCounter deliverCounter;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update() {
         spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0) {
            if(waitingRecipeSOList.Count < maxMission){
                RecipeSO waitingRecipeSO = recipeListSO.recipes[UnityEngine.Random.Range(0, recipeListSO.recipes.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                Debug.Log(waitingRecipeSO.recipeName);
                spawnTimer = spawnTimerCD;
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject receivedPlate) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++){
            RecipeSO waitingRecipe = waitingRecipeSOList[i];

            if(waitingRecipe.kitchenSoList.Count == receivedPlate.GetKitchenSOList().Count){
                // Has the same number of ingredent
                bool plateContentsMatchesRecipe = true;;
                foreach(KitchenSO ingredent in receivedPlate.GetKitchenSOList()){
                    bool ingredentFound = false;
                    foreach(KitchenSO waitingIngredent in waitingRecipe.kitchenSoList){
                        if(ingredent == waitingIngredent){
                            ingredentFound = true;
                            break;
                        }
                    }
                    if(!ingredentFound){
                        // This plate doesn't match
                        plateContentsMatchesRecipe = false;
                    }
                }

                if(plateContentsMatchesRecipe){
                    Debug.Log("Nice!!");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("Wrong recipe!!");
    }

    public List<RecipeSO> GetWaitingList(){
        return waitingRecipeSOList;
    }
}
