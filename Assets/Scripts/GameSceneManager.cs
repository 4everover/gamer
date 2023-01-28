using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Gameplay");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }

    public void TimeLoadMenu()
    {
        StartCoroutine(MenuTimeLoad());
    }
    IEnumerator MenuTimeLoad()
    {
        yield return new WaitForSeconds(7);
        LoadMenu();
    }

    public void LoadDemo()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("TechDemo");
    }

    public void TimeLoadEndscreen()
    {
        StartCoroutine(EndTimeLoad());
    }

    IEnumerator EndTimeLoad()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("EndScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
