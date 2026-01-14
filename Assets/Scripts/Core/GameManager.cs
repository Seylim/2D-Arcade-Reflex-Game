using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    private float remainingTime;
    private int score;

    public GameState CurrentState { get; private set; }

    void Start()
    {
        
    }

    private void StartGame()
    {
        score = 0;
        remainingTime = gameConfig.gameDuration;
        CurrentState = GameState.Playing;

        GameEvents.OnGameStarted?.Invoke();
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    void Update()
    {
        if (CurrentState != GameState.Playing)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        CurrentState = GameState.GameOver;
        GameEvents.OnGameOver?.Invoke();
    }

    public void AddScore(int amount)
    {
        score += amount;
        GameEvents.OnScoreChanged?.Invoke(score);
    }
}
