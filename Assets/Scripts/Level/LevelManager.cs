using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

	// --VARIABLES --
	public int deathCounter { get; set; } = 0;
	public Collider2D CurrentLevelArea { get; set; }

    [SerializeField] TMP_Text deathCountText;
	public List<LevelCollider> cameraLevelsPos = new List<LevelCollider>();
    public Transform FirstSpawn;
    public BasePlayer Player { get; set; }
    [SerializeField] private GameObject PlayerPrefab;
    

    public Dictionary<string, Sprite> characterPortraits = new Dictionary<string, Sprite>();

    public void OnEnable()
    {
        GameEvents.OnPlayerDied += IncreaseDeathCounter;
    }

	public void OnDisable()
	{
        GameEvents.OnPlayerDied -= IncreaseDeathCounter;
	}

    void IncreaseDeathCounter() 
    {
        deathCounter++;
        deathCountText.text = deathCounter.ToString();
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
