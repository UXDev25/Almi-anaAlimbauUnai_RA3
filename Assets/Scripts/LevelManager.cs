using UnityEngine;

public class LevelManager : MonoBehaviour
{
    LevelManager Instance;

    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
