using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDialogueTrigger : MonoBehaviour
{
    [SerializeField] bool isStartingDialogue;

    [SerializeField] Vector3 areaSize;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject dialogueToTrigger;

    static bool dialogueOcurring = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, areaSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isStartingDialogue) StartCoroutine(DoDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartingDialogue) return;

        Collider[] player = Physics.OverlapBox(transform.position, areaSize / 2f, Quaternion.identity, playerLayer);
        if (player.Length < 1) return;
        if (player[0].GetComponent<PlayerController>() && !dialogueOcurring)
        {
            StartCoroutine(DoDialogue());
        }
    }

    IEnumerator DoDialogue()
    {
        dialogueOcurring = true;
        dialogueBackground.SetActive(true);
        dialogueToTrigger.SetActive(true);
        yield return new WaitForSeconds(3);
        dialogueBackground.SetActive(false);
        Destroy(dialogueToTrigger);
        dialogueOcurring = false;
        Destroy(gameObject);
    }
}
