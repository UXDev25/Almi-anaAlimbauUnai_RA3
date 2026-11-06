using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<LevelCollider> cameraLevelsPos = new List<LevelCollider>();
    public Collider2D CurrentLevelArea { get; set; }
    public Transform FirstSpawn;

    void OnEnable()
    {
        GameEvents.OnPlayerDied += ResetGame;
    }

    void OnDisable()
    {
		GameEvents.OnPlayerDied -= ResetGame;
	}

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
            cameraLevelsPos.Add(transform.GetChild(i).gameObject.GetComponent<LevelCollider>());
            Debug.Log(cameraLevelsPos[i].gameObject.name);
        }
    }

    public void ResetGame() 
    {
        
    }
    public void ChangeLevelCamera(int index)
    {
        Camera.main.gameObject.transform.position = cameraLevelsPos[index].transform.position;
	}
}
