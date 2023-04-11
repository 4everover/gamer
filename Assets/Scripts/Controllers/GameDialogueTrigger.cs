using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialogueTrigger : MonoBehaviour
{
    [SerializeField] int dialogueNum;
    [SerializeField] GameObject[] dialogueToTrigger;
    [SerializeField] bool beatEnemiesToProgress = false;
    [SerializeField] Vector3 borderDimensions;
    [SerializeField] GameObject border;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool unlockNewStage = false;

    //[SerializeField] bool unlockStage3 = false;
    //[SerializeField] LayerMask playerLayer;

    bool borderPlaced = false;
    bool playerInArea = false;

    //BoxCollider triggerCollider;

    GameStoryManager gameStoryManager;

    private void OnDrawGizmosSelected()
    {
        if (!beatEnemiesToProgress) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(borderDimensions.x, borderDimensions.y, borderDimensions.z));
        //Gizmos.DrawWireCube(transform.position, new Vector3(35,35,35));
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStoryManager = FindObjectOfType<GameStoryManager>();
        //triggerCollider.GetComponent<BoxCollider>();
        if (border) { border.transform.position = new Vector3(1000, 0, 0); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!beatEnemiesToProgress) return;
        Collider[] enemies = Physics.OverlapBox(transform.position, new Vector3(borderDimensions.x/2, borderDimensions.y/2, borderDimensions.z/2), Quaternion.identity, enemyLayer);
        //Collider[] players = Physics.OverlapBox(transform.position, new Vector3(borderDimensions.x/2, borderDimensions.y/2, borderDimensions.z/2), Quaternion.identity, playerLayer);
        //Debug.Log(enemies.ToString());
        if (!borderPlaced && playerInArea)
        {
            borderPlaced = true;
            border.transform.position = transform.position;
        }
        //foreach (var e in enemies) { Debug.Log(e.name); }
        if (enemies.Length == 0) 
        {
            Destroy(border);
            gameStoryManager.DoDialogue(dialogueToTrigger, unlockNewStage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collidingObject)
    {
        if (!collidingObject.GetComponent<PlayerController>()) { return; }
        if (beatEnemiesToProgress)
        {
            playerInArea = true;
            return;
        }
        
        //Debug.Log("collision occurred");
        //triggerCollider.enabled = false;
        gameStoryManager.DoDialogue(dialogueToTrigger, unlockNewStage);
        if (!beatEnemiesToProgress) Destroy(gameObject);
    }
}
