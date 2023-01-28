using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAttack : MonoBehaviour
{
    RabbitAnimation rabbitAnimation;
    //RabbitMovement rabbitMovement;
    RabbitController rabbitController;

    Animator animator;

    [SerializeField] float minWaitForAttack = 1;
    [SerializeField] float maxWaitForAttack = 2;
    
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float attackDamage = 10;

    bool isAttacking = false;

    bool in_attack_loop = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        //rabbitMovement = GetComponent<RabbitMovement>();
        rabbitController = GetComponent<RabbitController>();
        rabbitAnimation = GetComponent<RabbitAnimation>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( rabbitController.RabbitCanAttack()) // rabbitMovement.RabbitCanAttack() ||
        {
            if (!in_attack_loop) 
            {
                StartCoroutine(AttackLoop()); 
            }
        }
        else 
        {
            StopAllCoroutines();
            in_attack_loop = false;
        }
    }

    IEnumerator AttackLoop()
    {
        in_attack_loop = true;
        var randWaitTime = Random.Range(minWaitForAttack, maxWaitForAttack);
        yield return new WaitForSeconds(randWaitTime);
        rabbitAnimation.DoAttackAnimation();
        yield return new WaitUntil(() => !isAttacking);
        in_attack_loop = false;
    }

    void Attack() // animation event
    {
        Collider[] playerHit = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);
        if (playerHit.Length > 0)
        {
            //Debug.Log(name + " hit " + playerHit[0].name);
            playerHit[0].GetComponent<Health>().TakeDamage(attackDamage);
        }
    }

    void SetIsAttacking(int i)
    {
        if (i == 1) { isAttacking = true; }
        else { isAttacking = false; }
    }
}
