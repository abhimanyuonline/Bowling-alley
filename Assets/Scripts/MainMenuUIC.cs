using TMPro;
using UnityEngine;

public class MainMenuUIC : MonoBehaviour
{
    BowllingGameManager GM;
    public TMP_InputField userName;
    public GameObject infoPanel;
    void Awake()
    {
        GM = BowllingGameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;
    }
    public void HandleOnStateChange()
    {
        GM.SetGameState(GameState.MAIN_MENU);

    }
    public void StartButton()
    {
        GameFlowManager.instance.StartBallGame();
        GM = BowllingGameManager.Instance;
        GM.OnStateChange += UpdateStateChange;

    }
    public void UpdateStateChange()
    {
        GM.SetGameState(GameState.GAME_MODE);
        Debug.Log("Current game state: " + GM.gameState);
    }
    public void SubmitUserName()
    {
        infoPanel.SetActive(false);
        DataManager.Instance.PlayerName = userName.text;
        ToastManager.Instance.ShowTost("Wellcome " + DataManager.Instance.PlayerName);
    }
    public void OptionButton()
    {

    }
    public void Quit()
    {

    }
}
