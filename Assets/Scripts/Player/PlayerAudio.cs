using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip spinAttackSound;
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

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
    public void PlaySpinAttackSound()
    {
        audioSource.clip = spinAttackSound;
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
    public AudioClip GetDieSound()
    {
        return dieSound;
    }
}
