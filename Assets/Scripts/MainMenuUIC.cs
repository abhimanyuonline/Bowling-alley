using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIC : MonoBehaviour
{
    SimpleGameManager GM;
    public TMP_InputField userName;
    public GameObject infoPanel;
    void Awake()
    {
        GM = SimpleGameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;
    }
     public void HandleOnStateChange ()
    {
        GM.SetGameState(GameState.MAIN_MENU);
       
    }
    public void StartButton()
    {
        GameFlowManager.instance.StartBallGame();
        GM = SimpleGameManager.Instance;
        GM.OnStateChange += UpdateStateChange;

    }
    public void UpdateStateChange(){
        GM.SetGameState(GameState.GAME_MODE);
        Debug.Log("Current game state: " + GM.gameState);
    } 
    public void SubmitUserName(){
        infoPanel.SetActive(false);
        DataManager.Instance.PlayerName = userName.text;
        ToastManager.Instance.ShowTost("Wellcome "+DataManager.Instance.PlayerName );
    }
    public void OptionButton()
    {

    }
    public void Quit()
    {

    }
}
