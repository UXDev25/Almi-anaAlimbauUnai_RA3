using UnityEngine;

public class WonCollider : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.PlayerWon();
        }
    }
}
