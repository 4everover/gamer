using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnd : MonoBehaviour
{
    [SerializeField] bool isOnEndScreen;
    [SerializeField] bool loadMenu;
    [SerializeField] bool loadStage3;
    [SerializeField] AudioClip clip;

    // Start is called before the first frame update
    void Awake()
    {
        if (isOnEndScreen)
        {
            if (loadMenu) FindObjectOfType<GameSceneManager>().TimeLoadMenu();
            else if (loadStage3) FindObjectOfType<GameSceneManager>().TimeLoadStage3();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            if (loadMenu) FindObjectOfType<GameSceneManager>().TimeLoadEndscreen();
            else if (loadStage3) FindObjectOfType<GameSceneManager>().TimeLoadEndscreen2();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
