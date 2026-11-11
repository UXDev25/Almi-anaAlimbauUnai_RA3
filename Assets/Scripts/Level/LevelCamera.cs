using UnityEngine;

public class LevelCamera : MonoBehaviour
{
	[SerializeField] private float smoothTime = 0.25f;

	LevelCollider currentLevel;
	private Vector3 moveCamvelocity;

	void LateUpdate()
	{
		foreach (var level in LevelManager.Instance.cameraLevelsPos)
		{
			if (level.thisCollider.OverlapPoint(LevelManager.Instance.Player.transform.position))
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
