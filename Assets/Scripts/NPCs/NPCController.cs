using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject dialogueDisplay;
    [Header("Will set facePlayer to true automatically if hasDialogue is true")]
    [SerializeField] bool facePlayer;
    [SerializeField] bool hasDialogue;
    [SerializeField] string dialogue = "A few years ago I was a few years old.";
    //[SerializeField] bool facePlayerIfClose;
    [SerializeField] float rangeToFacePlayer = 3;

    Rigidbody npcRigidbody;
    Quaternion originalRotation;

    TMP_Text dialogueDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
        originalRotation = transform.localRotation;
        dialogueDisplayText = dialogueDisplay.GetComponentInChildren<TMP_Text>();
        dialogueDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (facePlayer || hasDialogue)
        {
            var distanceToPlayer = Mathf.Abs(Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position));
            var distanceVector = FindObjectOfType<PlayerController>().transform.position - transform.position;


            if (distanceToPlayer <= rangeToFacePlayer)
            {
                if (hasDialogue)
                {
                    dialogueDisplayText.text = dialogue;
                    dialogueDisplay.SetActive(true);
                }
                Quaternion rotation = Quaternion.LookRotation(distanceVector);
                Quaternion current = transform.localRotation;
                transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 5);
            }
            else
            {
                dialogueDisplay.SetActive(false);
                Quaternion rotation = originalRotation;
                Quaternion current = transform.localRotation;
                transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 5);
            }
        }
    }
}
