using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip battleMusic;
    [SerializeField] AudioClip buttonSoundEffect;

    RabbitBossController[] rabbitBosses;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "TechDemo")
        {
            playMenuMusic();
        }
        else if (SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Map2")
        {
            playGameMusic();
            rabbitBosses = FindObjectsOfType<RabbitBossController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Map2")
        {
            //rabbitBosses = FindObjectsOfType<RabbitBossController>();
            foreach (var boss in rabbitBosses)
            {
                if (boss != null && boss.IsAggravated())
                {
                    playBattleMusic();
                    return;
                }
            }
            if (audioSource.clip == battleMusic)
            {
                playGameMusic();
            }
        }
    }

    public void playMenuMusic()
    {
        if (audioSource.clip == menuMusic) return;

        audioSource.loop = true;
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void playGameMusic()
    {
        if (audioSource.clip == gameMusic) return;
        if (!(SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Map2")) { return; }

        audioSource.loop = true;
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    public void playBattleMusic()
    {
        if (audioSource.clip == battleMusic) return;
        if (!(SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Map2")) { return; }

        audioSource.loop = true;
        audioSource.clip = battleMusic;
        audioSource.Play();
    }

    public void playButtonSFX()
    {
        AudioSource.PlayClipAtPoint(buttonSoundEffect, Camera.main.transform.position);
    }
}
