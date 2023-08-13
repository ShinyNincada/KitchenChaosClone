using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    private void Start() {
        if(DeliverManager.Instance.deliverCounter == null){
            DeliverManager.Instance.deliverCounter = this;
        }
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()){
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)){
                
                DeliverManager.Instance.DeliveryRecipe(plate);
                //Only accept plates
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
