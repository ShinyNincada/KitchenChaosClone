using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameStateChanged;
    public static GameManager Instance { get; private set; }
    GAMESTATE state;
    public float waitingToStartTimer = 5;
    public float countdownToStartTimer = 3;
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }
        else{
            Debug.LogError("Another instance existed!!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
                    countdownToStartTimer = 3f;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);                    
                }
                break;
            case GAMESTATE.Playing:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0 ){
                    state = GAMESTATE.Playing;
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
}
    public enum GAMESTATE{
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }