using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
	public static TransitionManager Instance;

	// -- SceneNames
	public const string MAIN_MENU_SCENE = "MainMenu";
	public const string GAME_SCENE = "Level";

	// -- Components
	/*[SerializeField] private Slider progressSlider;
	[SerializeField] private GameObject transitionItem;*/

	// -- Load times
	[SerializeField] private float animationWaitTime = 0.5f;
	[SerializeField] private float initialProgressTime = 1f;

	private Animator transitionAnimator;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		//Init();
		DontDestroyOnLoad(gameObject);
	}

	/*void Init()
	{
		transitionAnimator = transitionItem.GetComponent<Animator>();
	}*/

	// -- Scene Loading
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
		//AudioManager.Instance.StopMusic();
		//StartCoroutine(LoadRoutine(sceneName));
	}


	// --TODO:
	/*
	private IEnumerator LoadRoutine(string sceneName)
	{
		transitionItem.SetActive(true);
		transitionAnimator.SetBool("HasLoaded", false);
		yield return new WaitForSeconds(animationWaitTime);

		progressSlider.gameObject.SetActive(true);

		UpgradeProgressValue(0f);
		yield return new WaitForSeconds(initialProgressTime);
		var sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

		while (!sceneAsync.isDone)
		{
			UpgradeProgressValue(sceneAsync.progress);
			yield return null;
		}
		UpgradeProgressValue(1);
		progressSlider.gameObject.SetActive(false);
		transitionAnimator.SetBool("HasLoaded", true);
		yield return new WaitForSeconds(animationWaitTime);
		transitionAnimator.SetBool("HasLoaded", false);
		transitionItem.SetActive(false);
	}

	void UpgradeProgressValue(float progressValue)
	{
		progressSlider.value = progressValue;
	}
	*/
}