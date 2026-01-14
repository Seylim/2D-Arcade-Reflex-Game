using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject gameOverPanel;

    private void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScore;
        GameEvents.OnGameOver += ShowGameOver;
        GameEvents.OnGameRestarted += HideGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
        GameEvents.OnGameOver -= ShowGameOver;
        GameEvents.OnGameRestarted -= HideGameOver;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnRestartButtonClicked()
    {
        GameEvents.OnGameRestarted?.Invoke();
    }
}
