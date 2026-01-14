using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    private float remainingTime;
    private int score;

    public GameState CurrentState { get; private set; }

    private void OnEnable()
    {
        GameEvents.OnTargetHit += HandleTargetHit;
        GameEvents.OnGameRestarted += RestartGame;
    }

    private void OnDisable()
    {
        GameEvents.OnTargetHit -= HandleTargetHit;
        GameEvents.OnGameRestarted -= RestartGame;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        score = 0;
        remainingTime = gameConfig.gameDuration;
        CurrentState = GameState.Playing;

        GameEvents.OnGameStarted?.Invoke();
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    private void Update()
    {
        if (CurrentState != GameState.Playing)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
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

    private void HandleTargetHit(int scoreValue)
    {
        if (CurrentState != GameState.Playing)
            return;

        AddScore(scoreValue);
    }

    private void RestartGame()
    {
        score = 0;
        remainingTime = gameConfig.gameDuration;
        CurrentState = GameState.Playing;

        GameEvents.OnGameStarted?.Invoke();
        GameEvents.OnScoreChanged?.Invoke(score);
    }
}
