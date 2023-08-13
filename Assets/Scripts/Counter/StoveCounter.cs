using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter: BaseCounter,  IHasProgress
{
    public enum STOVESTATE {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public STOVESTATE state;
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private KitchenSO fryKitchenObjectSO;
    public float fryingTimer;
    public float burningTimer;
    [SerializeField] StoveCounter.STOVESTATE state;
    private FryingRecipeSO recipeSO;

    private void Start() {
        state = STOVESTATE.Idle;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
            state = state
        });
    }
    private void Update() {
       if(HasKitchenObject()){
            switch (state){
                case STOVESTATE.Idle: 
                
                    break;
                case STOVESTATE.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float) fryingTimer / recipeSO.fryingTimerMax
                    });
                    if(recipeSO != null && fryingTimer >= recipeSO.fryingTimerMax)
                    {
                        fryingTimer = 0;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = (float) fryingTimer / recipeSO.fryingTimerMax
                        });
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(recipeSO.output, this);
                        recipeSO = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenSO());
                        Debug.Log("Fried");
                        if(recipeSO != null) {
                            state = STOVESTATE.Fried;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                                state = state
                            });
                        }
                        burningTimer = 0;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = (float) burningTimer / recipeSO.fryingTimerMax
                        });
                    }
                    break;
                case STOVESTATE.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = (float) burningTimer / recipeSO.fryingTimerMax
                    });
                    if(burningTimer >= recipeSO.fryingTimerMax){
                        //Burned
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(recipeSO.output, this);

                        state = STOVESTATE.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });

                    }
                    break;
                case STOVESTATE.Burned:
                    break;
                default:
                    throw new NotImplementedException("Case not implemented");
                    
            }
       }
    }

    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
            //There is no kitchenObject here
            if(player.HasKitchenObject()){
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenSO()))
                {
                    //Player carry smthing that can be fry
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    recipeSO = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenSO());

                    state = STOVESTATE.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    fryingTimer = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float) fryingTimer / recipeSO.fryingTimerMax
                    });
                }
            }
            else{
                //Player doesn't has anything

            }
        }
        else{
            //There is a kitchenObject here
            if(player.HasKitchenObject()){
                // The player is holding something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)){
                    //Player is holding a plate
                    if(plate.TryAddIngredent(GetKitchenObject().GetKitchenSO())){
                        GetKitchenObject().DestroySelf();

                        state = STOVESTATE.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                                state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else{
                //Player not carrying anthing
                GetKitchenObject().SetKitchenObjectParent(player);

                state = STOVESTATE.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = 0f
                });
            }   
        }
    }

    private KitchenSO GetOutputForInput(KitchenSO input){
        FryingRecipeSO recipeSO = GetFryingRecipeWithInput(input);
        if(recipeSO != null){
            return recipeSO.output;
        }
        else return null;
    }

    private bool HasRecipeWithInput(KitchenSO inputKitchenObjectSO){
        FryingRecipeSO recipeSO = GetFryingRecipeWithInput(inputKitchenObjectSO);
        return recipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenSO input){
        foreach(FryingRecipeSO recipe in fryingRecipes){
            if(input == recipe.input){
                return recipe;
            }
        }
        return null;
    }
}