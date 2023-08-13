using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleDeliveryItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeName(RecipeSO recipe){
        recipeNameText.text = recipe.recipeName;

        foreach (Transform child in iconContainer){
            if (child == iconTemplate) continue;
            else Destroy(child.gameObject);
        }

        foreach (KitchenSO item in recipe.kitchenSoList){
            Transform spawnedIcon = Instantiate(iconTemplate, iconContainer);
            spawnedIcon.gameObject.SetActive(true);
            spawnedIcon.GetComponent<Image>().sprite = item.sprite;
        }
    }
}
