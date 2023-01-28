using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBossAnimation : MonoBehaviour
{
    readonly string IS_AGGRO = "isAggro";
    readonly string ATTACK_PLAYER = "attackPlayer";
    readonly string CAN_ATTACK_PLAYER = "canAttackPlayer";



    //RabbitMovement rabbitMovement;
    RabbitBossController rabbitBossController;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //rabbitMovement = GetComponent<RabbitMovement>();
        rabbitBossController = GetComponent<RabbitBossController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rabbitBossController.IsAggravated()) // rabbitMovement.IsAggravated() || 
        {
            animator.SetBool(IS_AGGRO, true);
            if (rabbitBossController.RabbitCanAttack()) // rabbitMovement.RabbitCanAttack() || 
            {
                animator.SetBool(CAN_ATTACK_PLAYER, true);
            }
            else
            {
                animator.SetBool(CAN_ATTACK_PLAYER, false);
            }
        }
        else
        {
            animator.SetBool(IS_AGGRO, false);
        }
    }

    public void DoAttackAnimation()
    {
        animator.SetTrigger(ATTACK_PLAYER);
    }
}
