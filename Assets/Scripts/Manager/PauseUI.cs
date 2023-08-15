using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

    
public class PauseUI : MonoBehaviour {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button mainMenuButton;
    private void Awake() {

    }
    // [SerializeField] private Button resumeButton;
    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        resumeButton.onClick.AddListener(() => GameManager.Instance.ToggleGamePause());
        mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));
        OptionsButton.onClick.AddListener(() => OptionUI.Instance.Show());
        
        Hide();
    }



    private void GameManager_OnGamePaused(object sender, EventArgs e){
        Show();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e) {
        Hide();
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