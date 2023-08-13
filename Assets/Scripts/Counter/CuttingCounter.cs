using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;   
    public event EventHandler OnCut;
    public static EventHandler OnAnyCut;
    [SerializeField] private CuttingRecipe[] cuttingRecipes;
    [SerializeField] private KitchenSO cutKitchenObjectSO;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
            //There is no kitchenObject here
            if(player.HasKitchenObject()){
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenSO()))
                {
                    //Player carry smthing that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipe recipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float) cuttingProgress / recipeSO.cuttingProgressMax
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
                    }
                }
            }
            else{
                //Player not carrying anthing
                GetKitchenObject().SetKitchenObjectParent(player);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = 0f
                });
            }   
        }
    }

    public override void InteractAlternative(Player player)
    {
        if(HasKitchenObject()){
             //There is a Kitchen Object here
            if(GetOutputForInput(GetKitchenObject().GetKitchenSO())){
                //And it can be cut

                 cuttingProgress++;

                CuttingRecipe recipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = (float) cuttingProgress / recipeSO.cuttingProgressMax
                });

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
            
                if(recipeSO != null && cuttingProgress >= recipeSO.cuttingProgressMax)
                {
                    KitchenSO output = GetOutputForInput(GetKitchenObject().GetKitchenSO());
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(output, this);
                }
        
            }
        }
    }

    private KitchenSO GetOutputForInput(KitchenSO input){
        CuttingRecipe recipeSO = GetCuttingRecipeWithInput(input);
        if(recipeSO != null){
            return recipeSO.output;
        }
        else return null;
    }

    private bool HasRecipeWithInput(KitchenSO inputKitchenObjectSO){
        CuttingRecipe recipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        return recipeSO != null;
    }

    private CuttingRecipe GetCuttingRecipeWithInput(KitchenSO input){
        foreach(CuttingRecipe recipe in cuttingRecipes){
            if(input == recipe.input){
                return recipe;
            }
        }
        return null;
    }
}
