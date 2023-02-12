using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pausebutton;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pausebutton.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _pausebutton.SetActive(true);
    }
}
