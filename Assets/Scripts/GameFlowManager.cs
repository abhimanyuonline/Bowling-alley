using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instance;
    public Canvas uiCanvas;
    public GameObject gameSpawner;
    private int _maxFPS = 60;
    public int maxFPS
    {
        get { return _maxFPS; }
        set
        {
            Application.targetFrameRate = value;
            _maxFPS = value;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        DestroyImmediate(this.gameObject);
        SetApplicationProperties();
    }

    void Start()
    {
        EnterMainMenu();
    }
    public void SaveName()
    {

    }
    public void EnterMainMenu()
    {

    }
    public void StartBallGame()
    {
        InitSceneSetupForGame();
    }
    void InitSceneSetupForGame()
    {
        gameSpawner.SetActive(true);
        SetUpGame.instance.InitSceneSetupForGame();
        SetUpGame.instance.userName.text = DataManager.Instance.PlayerName;
        uiCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void SetApplicationProperties()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = maxFPS;
    }
}
