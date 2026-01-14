using System;

public static class GameEvents
{
    public static Action OnGameStarted;
    public static Action OnGameOver;
    public static Action<int> OnScoreChanged;
}
