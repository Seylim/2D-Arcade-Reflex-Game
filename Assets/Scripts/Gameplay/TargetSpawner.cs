using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPrefab;
    [SerializeField]
    private GameConfig config;

    private void OnEnable()
    {
        GameEvents.OnGameStarted += StartSpawning;
        GameEvents.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStarted -= StartSpawning;
        GameEvents.OnGameOver -= StopSpawning;
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnTarget();
            yield return new WaitForSeconds(config.spawnInterval);
        }
    }

    private void SpawnTarget()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(-4f, 4f),
            Random.Range(-4f, 4f)
        );

        Instantiate(targetPrefab, randomPos, Quaternion.identity);
    }
}
