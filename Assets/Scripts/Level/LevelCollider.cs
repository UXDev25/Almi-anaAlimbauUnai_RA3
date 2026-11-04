using System;
using UnityEngine;

public class LevelCollider : MonoBehaviour
{

    [SerializeField] private LevelConnection connection;

    [SerializeField] private int firstLevelLink;
    [SerializeField] private int secondLevelLink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             LevelConnection.ActiveConnection = connection;
             LevelManager.Instance.ChangeLevelCamera(LevelManager.Instance.CurrentLevelIndex == firstLevelLink ? secondLevelLink : firstLevelLink); 
        }
    }
}
