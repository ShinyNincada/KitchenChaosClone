using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenSO kitchenObjectSO;


    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //There is no kitchenObject here
            if(player.HasKitchenObject()){
                //Player carry smthing
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                else{
                    //Player holding something not a plate
                    if(GetKitchenObject().TryGetPlate(out plate)){
                        //There is a plate on the table
                        if(plate.TryAddIngredent(player.GetKitchenObject().GetKitchenSO())){
                           player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else{
                //Player not carrying anthing
                GetKitchenObject().SetKitchenObjectParent(player);
            }   
        }
    }

   
}
