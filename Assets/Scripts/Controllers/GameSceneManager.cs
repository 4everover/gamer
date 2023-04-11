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

    public void LoadStage3()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Map2");
    }

    public void TimeLoadStage3()
    {
        StartCoroutine(Stage3TimeLoad());
    }

    IEnumerator Stage3TimeLoad()
    {
        yield return new WaitForSeconds(5);
        LoadStage3();
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

    public void TimeLoadEndscreen2()
    {
        StartCoroutine(EndTimeLoad2());
    }

    IEnumerator EndTimeLoad2()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt("Stage3Complete", 1);
        if (FindObjectOfType<GameSaveManager>())
        {
            FindObjectOfType<GameSaveManager>().SaveGame();
        }
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("EndScreen2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
