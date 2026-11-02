using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Dictionary<int, Transform> spawnPoints = new Dictionary<int, Transform>();
    public GameObject[] exits = new GameObject[0];
}
