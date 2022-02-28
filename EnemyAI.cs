using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;
    Animator anim;

    [SerializeField] float chaseDistance = 2;
    [SerializeField] float turnSpeed = 0.4f;
    public float damageAmount = 35f;
    [SerializeField] float attackTime = 4f;

    
    bool isDead = false;
    public bool canAttack = true;
    bool isAttacking;
   
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("AttackNearBy"));
 
       // Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > chaseDistance && !isDead)
        {
            ChasePlayer();
        }
        else if (canAttack && !PlayerHealth.singleton.isDead)
        {
            AttackPlayer();
        }
        else if(PlayerHealth.singleton.isDead)
        {
            DisableEnemy();
        }
    }

    void ChasePlayer() 
    {
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        anim.SetBool("isWalking", true); //when you get one true, anything elses shoule be false
        anim.SetBool("isAttacking", false);
    }

    void AttackPlayer() 
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
       // transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),turnSpeed*Time.deltaTime);
        agent.updatePosition = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        StartCoroutine((AttackTime()));
    }

    void DisableEnemy()
    {
        canAttack = false;
        anim.SetBool("isWalking", false); //when you get one true, anything elses shoule be false
        anim.SetBool("isAttacking", false);
    }



        public void EnemyDeathAnim() 
    {
        isDead = true;
        anim.SetTrigger("isDead");
        
    }



    IEnumerator AttackTime() 
    {
        canAttack = false;
        yield return new WaitForSeconds(2.1f);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AttackNearBy") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
        {
            
            PlayerHealth.singleton.PlayerDamage(35);
        }
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}