using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // -- SceneNames
    public const string MAIN_MENU_SCENE = "MainMenu";
    public const string GAME_SCENE = "Level";

    public static UIManager Instance;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject wonMenu;

    void OnEnable()
    {
        GameEvents.OnPlayerWon += ShowWonMenu;
        GameEvents.OnShowPauseMenu += ShowPauseMenu;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerWon -= ShowWonMenu;
        GameEvents.OnShowPauseMenu -= ShowPauseMenu;
    }
    void Start() => Time.timeScale = 1f;

    void ShowWonMenu() 
    {
        Time.timeScale = 0f;
        wonMenu.SetActive(true);
    } 

    void ShowPauseMenu(bool shouldShow) 
    {
        Time.timeScale = shouldShow ? 0f : 1f;
        pauseMenu.SetActive(shouldShow);    
    }

    // --MENU OPTIONS --
    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void ExitGame() => Application.Quit();
}
