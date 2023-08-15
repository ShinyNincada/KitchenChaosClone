using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Awake() {
        playBtn.onClick.AddListener(() => {
            //Click
            Loader.Load(Loader.Scene.GameScene);
        });

        quitBtn.onClick.AddListener(() => {
            // Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
