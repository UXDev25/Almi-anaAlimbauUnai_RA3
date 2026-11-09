using UnityEngine;

[CreateAssetMenu(fileName = "JosephData", menuName = "Scriptable Objects/JosephData")]
public class JosephData : ScriptableObject
{
	public float shootCooldown = 1.0f;
	public Vector2 shootDirection;
	public float shootForce = 5f;
}
