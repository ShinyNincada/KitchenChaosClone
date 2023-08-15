using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static void ResetStaticData(){
        OnAnyObjectPlacedHere = null;
    }
    public static event EventHandler OnAnyObjectPlacedHere;

    [SerializeField] private Transform spawnPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player){
        Debug.Log("Base");
    }

    public virtual void InteractAlternative(Player player){
        Debug.Log("Alternative Interact");
    }
    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public void SetKitChenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null){
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }


    public Transform GetKitchenObjectFollowTransform(){
        return spawnPoint;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

  
}
