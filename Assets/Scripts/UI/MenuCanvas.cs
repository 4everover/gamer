using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject playMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        playMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        playMenu.SetActive(false);
    }

    public void LoadOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void LoadPlayMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        playMenu.SetActive(true);
    }
}
