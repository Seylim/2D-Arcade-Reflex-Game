using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 10;
    private void OnMouseDown()
    {
        GameEvents.OnTargetHit?.Invoke(scoreValue);
        Destroy(gameObject);
    }
}
