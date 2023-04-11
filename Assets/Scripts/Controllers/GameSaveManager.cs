using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    [SerializeField] GameObject[] allSaveableGameObjectsInThisStage;

    SaveData saveData;
    bool gameHasStarted;

    // Start is called before the first frame update
    void Start()
    {
        gameHasStarted = false;
        saveData = new SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHasStarted) return;

        if (PlayerPrefs.GetFloat("SaveGameExists") == 1)
        {
            LoadGame();
        }
        else
        {
            SaveGame();
            PlayerPrefs.SetFloat("SaveGameExists", 1);
        }

        gameHasStarted = true;

        //if (Input.GetKeyDown(KeyCode.Alpha1)) SaveGame();
        //else if (Input.GetKeyDown(KeyCode.Alpha2)) LoadGame();
    }

    public void SaveGame()
    {
        Debug.Log("SAVING GAME...");

        //saveData.rabbits = new List<RabbitSaveProfile>();
        //saveData.rabbitBosses = new List<RabbitBossSaveProfile>();
        //saveData.pickableItems = new List<PickableItemSaveProfile>();

        for (int n = 0; n < allSaveableGameObjectsInThisStage.Length; n++)
        {
            var thing = allSaveableGameObjectsInThisStage[n];
            if (thing.GetComponent<PlayerController>())
            {
                saveData.playerSaveProfile = thing.GetComponent<PlayerController>().SavePlayer();
            }
            else if (thing.GetComponent<StageThreeManager>())
            {
                thing.GetComponent<StageThreeManager>().canCheckVillageClear = false;
                saveData.stageThreeProfile = thing.GetComponent<StageThreeManager>().SaveVillagesCleared();
                thing.GetComponent<StageThreeManager>().canCheckVillageClear = true;
            }
            
        }

        SaveSystem.Save(saveData);

        Debug.Log("GAME SAVED!");
    }

    public void LoadGame()
    {
        Debug.Log("LOADING GAME...");

        saveData = SaveSystem.Load();

        for (int n = 0; n < allSaveableGameObjectsInThisStage.Length; n++)
        {
            var thing = allSaveableGameObjectsInThisStage[n];
            if (thing.GetComponent<PlayerController>())
            {
                thing.GetComponent<PlayerController>().LoadPlayer(saveData);
            }
            else if (thing.GetComponent<StageThreeManager>())
            {
                thing.GetComponent<StageThreeManager>().canCheckVillageClear = false;
                thing.GetComponent<StageThreeManager>().LoadVillagesCleared(saveData);
                thing.GetComponent<StageThreeManager>().canCheckVillageClear = true;
            }
        }

        Debug.Log("GAME LOADED!");
    }
}
