using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOrPauseCanvas : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject dialogueCanvas;

    readonly string PAUSE_AXIS = "Pause";

    Health playerScript;
    PlayerAudio playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
        playerScript = FindObjectOfType<PlayerController>().GetComponent<Health>();
        playerAudio = FindObjectOfType<PlayerController>().GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.IsDead() && !deathScreen.activeSelf)
        {
            EnableDeathScreen();
        }
        else if (Input.GetButtonDown(PAUSE_AXIS) && !pauseScreen.activeSelf && !deathScreen.activeSelf)
        {
            PauseGame();
        }
        else if (Input.GetButtonDown(PAUSE_AXIS) && pauseScreen.activeSelf && !deathScreen.activeSelf)
        {
            UnpauseGame();
        }
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnpauseGame() 
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void EnableDeathScreen() 
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
        Destroy(dialogueCanvas);
        Cursor.lockState = CursorLockMode.None;
        AudioSource.PlayClipAtPoint(playerAudio.GetDieSound(), Camera.main.transform.position);
    }
}
