using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<GameObject> cameraLevelsPos = new List<GameObject>();
    public int CurrentLevelIndex { get; set; } = 0;

    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            cameraLevelsPos.Add(transform.GetChild(i).gameObject);
            Debug.Log(cameraLevelsPos[i].gameObject.name);
        }
    }

    public void ChangeLevelCamera(int index)
    {
        Camera.main.gameObject.transform.position = cameraLevelsPos[index].transform.position;
        CurrentLevelIndex = index;
	}
}
