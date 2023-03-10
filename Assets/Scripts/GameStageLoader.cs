using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageLoader : MonoBehaviour
{
    [SerializeField] GameObject stage2Button;

    static int stagesUnlocked = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("StagesUnlocked") < 1) PlayerPrefs.SetInt("StagesUnlocked", 1);
        stagesUnlocked = PlayerPrefs.GetInt("StagesUnlocked");
    }

    // Update is called once per frame
    void Update()
    {
        stagesUnlocked = PlayerPrefs.GetInt("StagesUnlocked");
        if (stagesUnlocked != 2) stage2Button.SetActive(false);
        else stage2Button.SetActive(true);
    }

    public void ResetStages()
    {
        PlayerPrefs.SetInt("StagesUnlocked", 1);
        stagesUnlocked = PlayerPrefs.GetInt("StagesUnlocked");
    }

    public void LoadStage1()
    {
        GameStoryManager.SetStageNumber(1);
    }

    public void LoadStage2()
    {
        GameStoryManager.SetStageNumber(2);
    }
}
