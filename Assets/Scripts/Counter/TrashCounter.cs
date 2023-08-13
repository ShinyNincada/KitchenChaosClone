using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    public static event EventHandler OnTrashDrop;
    
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()){
            player.GetKitchenObject().DestroySelf();
            OnTrashDrop?.Invoke(this, new EventArgs());
        }
    }
}
