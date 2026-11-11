using UnityEngine;

public class LevelCamera : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private float smoothTime = 0.25f;

	LevelCollider currentLevel;
	private Vector3 moveCamvelocity;

	void LateUpdate()
	{
		foreach (var level in LevelManager.Instance.cameraLevelsPos)
		{
			if (level.thisCollider.OverlapPoint(player.position))
			{
				currentLevel = level;
				break;
			}
		}

		if (currentLevel == null) return;

		Vector3 target = new Vector3(
			currentLevel.transform.position.x,
			currentLevel.transform.position.y,
			transform.position.z);

		transform.position = Vector3.SmoothDamp(transform.position, target, ref moveCamvelocity, smoothTime);
	}
}
