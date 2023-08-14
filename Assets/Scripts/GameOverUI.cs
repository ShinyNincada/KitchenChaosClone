using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

     private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManage_OnGameStateChanged;

        Hide();
    }

  

    private void GameManage_OnGameStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsGameOver()){
            Show();
            recipesDeliveredText.text = DeliverManager.Instance.GetSuccessRecipes().ToString();
        }
        else{
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {   
        gameObject.SetActive(true);
    }
}
