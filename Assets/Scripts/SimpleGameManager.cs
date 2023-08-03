using UnityEngine;
using System.Collections;
using System;

// Game States
// for now we are only using these two
public enum GameState { INTRO, MAIN_MENU,GAME_MODE }

public delegate void OnStateChangeHandler();

public class SimpleGameManager {
    protected SimpleGameManager() {}
    private static SimpleGameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public  GameState gameState { get; private set; }

    public static SimpleGameManager Instance{
        get {
            if (SimpleGameManager.instance == null){
                DontDestroyOnLoad(SimpleGameManager.instance);
                SimpleGameManager.instance = new SimpleGameManager();
            }
            return SimpleGameManager.instance;
        }

    }

    private static void DontDestroyOnLoad(SimpleGameManager instance)
    {
       // throw new NotImplementedException();
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit(){
        SimpleGameManager.instance = null;
    }

}