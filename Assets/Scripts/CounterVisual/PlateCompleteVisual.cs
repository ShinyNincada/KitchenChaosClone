using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenSO_Gameobject{
        public KitchenSO kitchenSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenSO_Gameobject> kitchenSO_GameobjectList;


    private void Start() {
        plateKitchenObject.OnIngredentAdded += PlateKitchenObject_OnIngredentAdded;
    }

    private void PlateKitchenObject_OnIngredentAdded(object sender, PlateKitchenObject.OnIngredentAddedEventArgs e)
    {
        foreach(KitchenSO_Gameobject item in kitchenSO_GameobjectList){
            if(item.kitchenSO == e.KitchenSO){
                item.gameObject.SetActive(true);
            }
        }
    }
}
