using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    
    private enum State{
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
[SerializeField] private Player player;

    private State state;
   private float waitingToStartTimer = 0.5f;
   private float countDownToStartTimer = 3;
   private float gamePlayingTimer = 200;
   private float gamePlaygTimeTotal;
private bool isGamePause = false;

    void Awake()
    {
        Instance = this;
        gamePlaygTimeTotal = gamePlayingTimer;
        
    }
private void Start(){
    TurnToWaitingToStart();
    GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
}

   private void GameInput_OnPauseAction(object sender, EventArgs e){
    ToggleGame();

    }
   

    void Update()
    {
        switch(state){
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer <= 0){
                    TurnToCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if(countDownToStartTimer <= 0){
                   TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0){
                   TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
                default:
                break;
        }
    }

private void TurnToWaitingToStart(){
state = State.WaitingToStart;
DisablePlayer();
    OnStateChanged?.Invoke(this, EventArgs.Empty);
}    
private void TurnToCountDownToStart(){
state = State.CountDownToStart;
DisablePlayer();
    OnStateChanged?.Invoke(this, EventArgs.Empty);
}
public void TurnToGamePlaying(){
    state = State.GamePlaying;
    EnablePlayer();
    OnStateChanged?.Invoke(this, EventArgs.Empty);
}
public void TurnToGameOver(){
    state = State.GameOver;
    OnStateChanged?.Invoke(this, EventArgs.Empty);
}

private void DisablePlayer(){
    player.enabled = false;
}

private void EnablePlayer(){
    player.enabled = true;
}

public bool IsWaitingToStartState(){
    return state == State.WaitingToStart;
}

public bool IsCountDownSate(){
    return state == State.CountDownToStart;
}


public bool IsGamePlayingState(){
    return state == State.GamePlaying;
}

public bool IsGameOverState(){
    return state == State.GameOver;
}

public float GetCountDownTimer(){
    return countDownToStartTimer;
}

public void ToggleGame(){
    isGamePause = !isGamePause;
    if(isGamePause){
        Time.timeScale = 0;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }
    else{
        Time.timeScale = 1;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }
}

public float GetGamePlayingTimer(){
    return gamePlayingTimer;
}

public float GetGamePlayingTimerNormalized(){
    return gamePlayingTimer / gamePlaygTimeTotal;
}
}

