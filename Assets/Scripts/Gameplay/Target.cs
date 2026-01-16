using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.STP;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 10;
    [SerializeField]
    private int lifeTime = 5;
    [SerializeField]
    private int subtractScoreOnMiss = 5;

    private Coroutine lifetimeCoroutine;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        lifetimeCoroutine = StartCoroutine(LifetimeRoutine());
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

        RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);

        if (hit.collider == null)
            return;

        if (hit.collider.gameObject != gameObject)
            return;

        GameEvents.OnTargetHit?.Invoke(scoreValue);
        StopCoroutine(lifetimeCoroutine);
        DestroyTarget();
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyTarget();
        GameEvents.OnTargetMissed?.Invoke(subtractScoreOnMiss);
    }

    private void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
