using System;
using UnityEngine;

public class LevelCollider : MonoBehaviour
{
    public Collider2D thisCollider { get; private set; }

    [SerializeField] private int levelId;

    void Start() 
    {
        thisCollider = GetComponent<Collider2D>();
    }

}
