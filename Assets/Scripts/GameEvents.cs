using System;
using UnityEngine;

public static class GameEvents
{
	// --EVENTS
	public static event Action OnPlayerDied;

	public static void PlayerDied()
	{
		OnPlayerDied?.Invoke();
	}
}
