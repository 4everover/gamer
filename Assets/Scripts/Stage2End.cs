using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2End : MonoBehaviour
{
    [SerializeField] bool isOnEndScreen;
    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Awake()
    {
        if (isOnEndScreen)
        {
            FindObjectOfType<GameSceneManager>().TimeLoadMenu();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            FindObjectOfType<GameSceneManager>().TimeLoadEndscreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
