using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<LevelCollider> cameraLevelsPos = new List<LevelCollider>();
    public Collider2D CurrentLevelArea { get; set; }
    public Transform FirstSpawn;
    public BasePlayer Player { get; set; }
    [SerializeField] private GameObject PlayerPrefab;

    public Dictionary<string, Sprite> characterPortraits = new Dictionary<string, Sprite>();

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
        }
        Player = Instantiate(PlayerPrefab).GetComponent<BasePlayer>();
    }
    public void ChangeLevelCamera(int index)
    {
        Camera.main.gameObject.transform.position = cameraLevelsPos[index].transform.position;
	}

    // YARN

    private void SearchForDialoguePortraits()
    {
        if (characterPortraits.Count == 0)
        {
            Sprite[] portraits = Resources.LoadAll<Sprite>("AllPortraits");
            foreach (Sprite portrait in portraits)
            {
                characterPortraits.Add(portrait.name, portrait);
            }
        }
    }
}
