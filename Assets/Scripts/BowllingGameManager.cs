public enum GameState
{
    INTRO,
    MAIN_MENU,
    GAME_MODE
}

public delegate void OnStateChangeHandler();

public class BowllingGameManager
{
    protected BowllingGameManager() { }
    private static BowllingGameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static BowllingGameManager Instance
    {
        get
        {
            if (BowllingGameManager.instance == null)
            {
                DontDestroyOnLoad(BowllingGameManager.instance);
                BowllingGameManager.instance = new BowllingGameManager();
            }
            return BowllingGameManager.instance;
        }

    }

    private static void DontDestroyOnLoad(BowllingGameManager instance)
    {
        // throw new NotImplementedException();
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit()
    {
        BowllingGameManager.instance = null;
    }

}