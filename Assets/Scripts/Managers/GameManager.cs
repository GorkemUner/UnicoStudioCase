using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private GameObject SuccessPanel;

    public UnityEvent OnGameBattleStart = new UnityEvent();
    private GameState gameState;
    public GameState GameState
    {
        get => gameState;
        set
        {
            gameState = value;
            if (gameState == GameState.OnBattle)
                OnGameBattleStart.Invoke();
            else if (gameState == GameState.GameFail)
                Instantiate(FailPanel, LevelManager.Instance.LevelInstance.transform);
            else if (gameState == GameState.GameSuccess)
                Instantiate(SuccessPanel, LevelManager.Instance.LevelInstance.transform);
        } 
    }

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameState = GameState.OnPreBattle;
    }

}