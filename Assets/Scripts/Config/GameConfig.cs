using UnityEngine;

[CreateAssetMenu(menuName = "Game/Config")]
public class GameConfig : ScriptableObject
{
    public float gameDuration = 30f;
    public float spawnInterval = 1f;
}
