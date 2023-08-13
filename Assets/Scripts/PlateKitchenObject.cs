using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredentAddedEventArgs> OnIngredentAdded;
    public class OnIngredentAddedEventArgs : EventArgs{
        public KitchenSO KitchenSO;
    }
    private List<KitchenSO> kitchenSOs;
    [SerializeField] private List<KitchenSO> validKitchenObjectSO;


    private void Awake() {
        kitchenSOs = new List<KitchenSO>();
    }

    public bool TryAddIngredent(KitchenSO kitchenSO){
        if(!validKitchenObjectSO.Contains(kitchenSO)){
            //Not valid
            return false;
        }
        if(kitchenSOs.Contains(kitchenSO)){
            //Already has this
            return false;
        }
        else{
            kitchenSOs.Add(kitchenSO);

            OnIngredentAdded?.Invoke(this, new OnIngredentAddedEventArgs{
                KitchenSO = kitchenSO
            });
            
            return true;
        }
    }

    public List<KitchenSO> GetKitchenSOList(){
        return kitchenSOs;
    }
    
}
