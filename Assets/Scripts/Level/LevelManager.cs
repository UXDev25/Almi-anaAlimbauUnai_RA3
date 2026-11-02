using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Level[] levels = new Level[0];
    private Level currentLevel;
    public GameObject playerPrefab;
    private int currentIndex;

    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != null) Destroy(currentLevel);
        currentIndex = index;
        currentLevel = Instantiate(levels[index]);
        currentLevel.gameObject.SetActive(true);
    }

    private void LoadPlayer() 
    {
        
    }
}
