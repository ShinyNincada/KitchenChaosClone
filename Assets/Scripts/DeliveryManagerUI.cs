using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    
    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliverManager.Instance.OnRecipeSpawned += DeliverManager_OnRecipeSpawned;
        DeliverManager.Instance.OnRecipeCompleted += DeliverManager_OnRecipeCompleted;

    }

    private void DeliverManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();

    }

    private void DeliverManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual(){
        foreach (Transform child in container){
            if (child == recipeTemplate) continue;
            else Destroy(child.gameObject);
        }

        foreach (RecipeSO recipe in DeliverManager.Instance.GetWaitingList()){
            Transform spawnedMission = Instantiate(recipeTemplate, container);
            spawnedMission.gameObject.SetActive(true);
            spawnedMission.GetComponent<SingleDeliveryItemUI>().SetRecipeName(recipe);
        }
    }
}
