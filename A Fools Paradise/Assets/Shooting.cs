using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playermovement;
    private float coolDownTimer = Mathf.Infinity;

   [SerializeField]private float attackCoolDown;


    private void Awake()
    {
       animator = GetComponent<Animator>();
       playermovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && playermovement.CanAttackIdle())
        {
            animator.SetBool("IsAttacking",true);
            animator.SetBool("NotAttacking", false);
            Attack();

        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("NotAttacking", true);
            animator.SetBool("IsAttacking", false);
        }




        if (Input.GetMouseButtonDown(0) && coolDownTimer > attackCoolDown && playermovement.CanAttackRun())
        {
            animator.SetBool("IsRunningAttack", true);
            Debug.Log("attack move");
            //Attack();
        }
        else
        {
            animator.SetBool("IsNotRunningAttack",true);
            animator.SetBool("IsRunningAttack", false);
        }

        coolDownTimer += Time.deltaTime;
    }


    void Attack() 
    {
        //animator.SetTrigger("Attack");
        coolDownTimer = 0f;
         
    }


}
