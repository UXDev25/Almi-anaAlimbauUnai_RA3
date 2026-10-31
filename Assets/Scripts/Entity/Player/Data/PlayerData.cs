using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float maxSpeed = 7f;
    public float acceleration = 0.5f;
    public float deceleration = 1.0f;
}
