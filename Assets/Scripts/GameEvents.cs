using System;

public static class GameEvents
{
	// --EVENTS
	public static event Action OnPlayerDied;
	public static event Action OnPlayerWon;
	public static event Action<bool> OnShowPauseMenu;

    public static void PlayerDied()
	{
		OnPlayerDied?.Invoke();
	}

	public static void PlayerWon()
    {
        OnPlayerWon?.Invoke();
    }

	public static void ShowPauseMenu(bool shouldShow)
    {
        OnShowPauseMenu?.Invoke(shouldShow);
    }
}
