using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

    
public class PauseUI : MonoBehaviour {
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    // [SerializeField] private Button resumeButton;
    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
    }
    
    private void GameManager_OnGamePaused(object sender, EventArgs e){
        throw new NotImplementedException();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e) {
        throw new NotImplementedException();
    }
}