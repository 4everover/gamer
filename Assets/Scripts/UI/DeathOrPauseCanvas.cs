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
        if (FindObjectOfType<InventoryCanvas>()) FindObjectOfType<InventoryCanvas>().HideInventory();
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnpauseGame() 
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void EnableDeathScreen() 
    {
        if (FindObjectOfType<InventoryCanvas>()) FindObjectOfType<InventoryCanvas>().HideInventory();
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        Destroy(dialogueCanvas);
        Cursor.lockState = CursorLockMode.None;
        AudioSource.PlayClipAtPoint(playerAudio.GetDieSound(), Camera.main.transform.position);
    }
}
