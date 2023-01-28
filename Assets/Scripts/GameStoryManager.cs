using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoryManager : MonoBehaviour
{
    static int stageNumber = 1;
    [SerializeField] GameObject[] stage1Items;
    [SerializeField] GameObject stage1MapSplittingBorder;
    [SerializeField] GameObject[] stage2Items;

    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject[] dialogueList;
    bool isDialogueOccurring = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Gameplay") return;
        Debug.Log(stageNumber);
        if (stageNumber == 2)
        {
            for (int i = stage1Items.Length - 1; i >= 0; i--)
            {
                Destroy(stage1Items[i]);
            }
            FindObjectOfType<PlayerController>().transform.position = new Vector3(51.5f, 4.5f, 20f);
        }
        else if (stageNumber == 1)
        {
            FindObjectOfType<PlayerController>().transform.position = new Vector3(61f, 4.5f, 98.75f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDialogue(GameObject[] d, bool unlockNewStage)
    {
        if (isDialogueOccurring) { return; }
        if (unlockNewStage)
        {
            stageNumber = 2;
            PlayerPrefs.SetInt("StagesUnlocked", 2);
            Destroy(stage1MapSplittingBorder);
        }
        StartCoroutine(DoThisDialogue(d));
    }

    public static void SetStageNumber(int i)
    {
        stageNumber = i;
    }
    public static int GetStageNumber()
    {
        return stageNumber;
    }


    IEnumerator DoThisDialogue(GameObject[] dList)
    {
        isDialogueOccurring = true;
        dialogueBackground.SetActive(true);
        foreach (var d in dList)
        {
            d.SetActive(true);
            yield return new WaitForSeconds(3);
            Destroy(d);
        }
        dialogueBackground.SetActive(false);
        isDialogueOccurring = false;
    }
}
