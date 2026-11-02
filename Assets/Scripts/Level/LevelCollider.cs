using UnityEngine;

public class LevelCollider : MonoBehaviour
{
    public int nextLevelIndex;
    public int thisLevelIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             LevelManager.Instance.LoadLevel(nextLevelIndex);
        }
    }
}
