using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManage_OnGameStateChanged;
        Hide();
    }

  

    private void GameManage_OnGameStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsCountDown()){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Update() {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
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
