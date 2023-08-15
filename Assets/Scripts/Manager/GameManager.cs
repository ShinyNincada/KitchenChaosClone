using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public static GameManager Instance { get; private set; }
    GAMESTATE state;
    public float waitingToStartTimer = 2;
    public float countdownToStartTimer = 3;
    public float playingTimer = 30f;
    public float playingTimerMax = 30f;
    private bool IsPaused = false;
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }
        else{
            Debug.LogError("Another instance existed!!");
        }
        state = GAMESTATE.WaitingToStart;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;   
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case GAMESTATE.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0 ){
                    state = GAMESTATE.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case GAMESTATE.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0 ){
                    state = GAMESTATE.Playing;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);                    
                }
                break;
            case GAMESTATE.Playing:
                playingTimer -= Time.deltaTime;
                if(playingTimer < 0 ){
                    state = GAMESTATE.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);                    
                }
                break;
            case GAMESTATE.GameOver:
                break;
        }
        Debug.Log("" + state);
    }

    public bool IsGamePlaying(){
        return state == GAMESTATE.Playing;
    }

    public bool IsCountDown(){
        return state == GAMESTATE.CountdownToStart;
    }

    public bool IsGameOver(){
        return state == GAMESTATE.GameOver;
    }
    public float GetCountDownTimer()
    {
        return countdownToStartTimer;
    }

    public void ToggleGamePause(){
        IsPaused = !IsPaused;

        if(IsPaused){
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else{
            Time.timeScale = 1;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetPlayingTimer(){
        return playingTimer;
    }
    public float GetPlayingTimerNormalize(){
        return 1 - (playingTimer / playingTimerMax);
    }
}
    public enum GAMESTATE{
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }