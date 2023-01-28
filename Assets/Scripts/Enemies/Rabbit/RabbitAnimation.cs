using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimation : MonoBehaviour
{
    readonly string IS_AGGRO = "isAggro";
    readonly string ATTACK_PLAYER = "attackPlayer";
    readonly string CAN_ATTACK_PLAYER = "canAttackPlayer";
    


    //RabbitMovement rabbitMovement;
    RabbitController rabbitController;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //rabbitMovement = GetComponent<RabbitMovement>();
        rabbitController = GetComponent<RabbitController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rabbitController.IsAggravated()) // rabbitMovement.IsAggravated() || 
        {
            animator.SetBool(IS_AGGRO, true);
            if (rabbitController.RabbitCanAttack()) // rabbitMovement.RabbitCanAttack() || 
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
