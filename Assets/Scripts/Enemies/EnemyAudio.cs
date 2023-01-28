using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip aggroSound;
    [SerializeField] AudioClip attackPlayerSound;
    [SerializeField] AudioClip getHitSound;
    [SerializeField] AudioClip dieSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAggroSound()
    {
        audioSource.clip = aggroSound;
        audioSource.Play();
    }
    public void PlayAttackPlayerSound()
    {
        audioSource.clip = attackPlayerSound;
        audioSource.Play();
    }
    public void PlayGetHitSound()
    {
        audioSource.clip = getHitSound;
        audioSource.Play();
    }
    public void PlayDieSound()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }
}
