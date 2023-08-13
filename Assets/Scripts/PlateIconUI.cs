using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform templateTransform;

    private void Start() {
        plateKitchenObject.OnIngredentAdded += plateKitchenObject_OnIngredentAdded;
        templateTransform.gameObject.SetActive(true);
    }

    private void plateKitchenObject_OnIngredentAdded(object sender, PlateKitchenObject.OnIngredentAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform){
            if (child == templateTransform) continue;
            else Destroy(child.gameObject);
        }
        foreach(var item in plateKitchenObject.GetKitchenSOList()){
            Transform newIcon = Instantiate(templateTransform, transform);
            newIcon.GetComponent<SingleImageUI>().UpdateIconImage(item.sprite);
        }
    }
}
