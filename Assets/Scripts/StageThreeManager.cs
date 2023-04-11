using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageThreeManager : MonoBehaviour
{
    [SerializeField] GameObject[] village1Rabbits;
    [SerializeField] GameObject[] village2Rabbits;
    [SerializeField] GameObject[] village3Rabbits;

    [SerializeField] GameObject[] v1AdditionalThingsToDestroy;
    [SerializeField] GameObject[] v2AdditionalThingsToDestroy;
    [SerializeField] GameObject[] v3AdditionalThingsToDestroy;

    [SerializeField] GameObject stage3EndText;

    [SerializeField] TMP_Text obj1;
    [SerializeField] TMP_Text obj2;
    [SerializeField] TMP_Text obj3;

    bool village1Cleared = false;
    bool village2Cleared = false;
    bool village3Cleared = false;

    public bool canCheckVillageClear = false;

    StageThreeProfile villageProfile;

    // Start is called before the first frame update
    void Start()
    {
        stage3EndText.SetActive(false);
        villageProfile = new StageThreeProfile();
    }

    // Update is called once per frame
    void Update()
    {
        if (village1Cleared)
        {
            for (int n = 0; n < village1Rabbits.Length; n++)
            {
                if (village1Rabbits[n] != null) Destroy(village1Rabbits[n]);
            }
            for (int n = 0; n < v1AdditionalThingsToDestroy.Length; n++)
            {
                if (v1AdditionalThingsToDestroy[n] != null) Destroy(v1AdditionalThingsToDestroy[n]);
            }
        }
        if (village2Cleared)
        {
            for (int n = 0; n < village2Rabbits.Length; n++)
            {
                Destroy(village2Rabbits[n]);
                if (village2Rabbits[n] != null) Destroy(village2Rabbits[n]);
            }
            for (int n = 0; n < v2AdditionalThingsToDestroy.Length; n++)
            {
                if (v2AdditionalThingsToDestroy[n] != null) Destroy(v2AdditionalThingsToDestroy[n]);
            }
        }
        if (village3Cleared)
        {
            for (int n = 0; n < village3Rabbits.Length; n++)
            {
                if (village3Rabbits[n] != null) Destroy(village3Rabbits[n]);
            }
            for (int n = 0; n < v3AdditionalThingsToDestroy.Length; n++)
            {
                if (v3AdditionalThingsToDestroy[n] != null) Destroy(v3AdditionalThingsToDestroy[n]);
            }
        }

        CheckVillageClearStatus();
    }

    void CheckVillageClearStatus()
    {
        if (canCheckVillageClear && (!village1Cleared || !village2Cleared || !village3Cleared))
        {
            var v1 = true;
            foreach (var rabbit in village1Rabbits)
            {
                if (rabbit != null)
                {
                    v1 = false;
                }
            }

            var v2 = true;
            foreach (var rabbit in village2Rabbits)
            {
                if (rabbit != null)
                {
                    v2 = false;
                }
            }
            //Debug.Log("Village 2 is " + village2Cleared);

            var v3 = true;
            foreach (var rabbit in village3Rabbits)
            {
                if (rabbit != null)
                {
                    v3 = false;
                }
            }

            village1Cleared = v1;
            village2Cleared = v2;
            village3Cleared = v3;
        }
        else if (village1Cleared && village2Cleared && village3Cleared)
        {
            // IF STAGE IS NOT BEING REPLAYED:
            // enable text saying all villages have been cleared, stage 3 completed
            // after 3 sec, load end screen
            if (PlayerPrefs.GetInt("Stage3Complete") != 1)
            {
                stage3EndText.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Stage3Complete") == 1)
            {
                if (stage3EndText != null) Destroy(stage3EndText);
            }

            // IF STAGE IS BEING REPLAYED:
            // delete the text and don't load the end screen
        }

        if (village1Cleared) obj1.text = "(COMPLETED) Clear the Mountain Village";
        if (village2Cleared) obj2.text = "(COMPLETED) Clear the Field Village";
        if (village3Cleared) obj3.text = "(COMPLETED) Clear the Forest Village";
    }

    public StageThreeProfile SaveVillagesCleared()
    {
        villageProfile.village1Cleared = BoolToInt(village1Cleared);
        villageProfile.village2Cleared = BoolToInt(village2Cleared);
        villageProfile.village3Cleared = BoolToInt(village3Cleared);

        Debug.Log(villageProfile.village1Cleared);
        Debug.Log(villageProfile.village2Cleared);
        Debug.Log(villageProfile.village3Cleared);

        return villageProfile;
    }

    public void LoadVillagesCleared(SaveData saveData)
    {
        villageProfile = saveData.stageThreeProfile;

        Debug.Log(villageProfile.village1Cleared);
        Debug.Log(villageProfile.village2Cleared);
        Debug.Log(villageProfile.village3Cleared);

        village1Cleared = IntToBool(villageProfile.village1Cleared);
        village2Cleared = IntToBool(villageProfile.village2Cleared);
        village3Cleared = IntToBool(villageProfile.village3Cleared);
    }

    bool IntToBool(int i)
    {
        if (i == 1) return true;
        else return false;
    }
    int BoolToInt(bool b)
    {
        if (b) return 1;
        else return 0;
    }
}
