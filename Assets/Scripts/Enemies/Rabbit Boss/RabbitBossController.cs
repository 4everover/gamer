using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitBossController : MonoBehaviour
{

    Rigidbody rabbitBossBody;
    CapsuleCollider rabbitBossCollider;
    NavMeshAgent agent;

    PlayerController player;

    Vector3 initialPos;

    [SerializeField] float playerDistanceToBeAggro = 7.5f;
    [SerializeField] float playerDistanceToAttack = 2.5f;

    bool canAttack = false;
    bool isAggravated = false;
    bool canMove = false;
    bool canRotate = false;

    float distanceBetweenPlayerAndRabbit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rabbitBossBody = GetComponent<Rigidbody>();
        rabbitBossCollider = GetComponent<CapsuleCollider>();
        initialPos = transform.position;

        player = FindObjectOfType<PlayerController>();
        playerDistanceToAttack = agent.stoppingDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player) { distanceBetweenPlayerAndRabbit = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)); }

        if (IsPlayerInRange())
        {
            FindObjectOfType<GameAudioManager>().playBattleMusic();

            isAggravated = true;
            if (canRotate) { FacePlayer(); }
            if (canMove) { agent.SetDestination(player.transform.position); }

            if (distanceBetweenPlayerAndRabbit <= agent.stoppingDistance)
            {
                //canMove = false;
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }
        }
        else
        {
            canMove = false;
            canAttack = false;
            isAggravated = false;
        }

    }

    void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    public bool RabbitCanAttack()
    {
        return canAttack;
    }

    bool IsPlayerInRange()
    {
        if (distanceBetweenPlayerAndRabbit <= playerDistanceToBeAggro) { return true; }
        else { return false; }
    }

    public bool IsAggravated() { return isAggravated; }

    // can move
    public void SetCanMove(int i)
    {
        if (i == 1) { canMove = true; }
        else { canMove = false; }
    }
    public bool GetCanMove() { return canMove; }

    // can rotate
    public void SetCanRotate(int i)
    {
        if (i == 1) { canRotate = true; }
        else { canRotate = false; }
    }
    public bool GetCanRotate() { return canRotate; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDistanceToBeAggro);
    }

}
