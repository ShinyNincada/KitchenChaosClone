using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenSO kitchenSO;
    private IKitchenObjectParent kitchenObjectParent;

   public KitchenSO GetKitchenSO(){
     return kitchenSO;
   }

    public void SetKitchenObjectParent(IKitchenObjectParent newParent){
          if(this.kitchenObjectParent != null){
               this.kitchenObjectParent.ClearKitchenObject();
          }
          this.kitchenObjectParent = newParent;
          newParent.SetKitChenObject(this);
          transform.parent = newParent.GetKitchenObjectFollowTransform();
          transform.localPosition = new Vector3(0, 0, 0);
    }
     public IKitchenObjectParent GetKithenObjectParent(){
          return this.kitchenObjectParent;
     }

     public void DestroySelf() {
          kitchenObjectParent.ClearKitchenObject();

          Destroy(gameObject);
     }

     public static KitchenObject SpawnKitchenObject(KitchenSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent){
          Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

          KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
          kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

          return kitchenObject;
     }

     public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
          if(this is PlateKitchenObject){
               plateKitchenObject = this as PlateKitchenObject;
               return true;
          }
          else {
               plateKitchenObject = null;
               return false;
          }
     }
     

   
}
