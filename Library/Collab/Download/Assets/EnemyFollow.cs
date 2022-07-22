using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public Animator anim;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        print("SHOFY EL PLAYER AHO");
        print(Player.position);
        agent = GetComponent<NavMeshAgent>();
        print("SHOFY EL AGENT AHO");
        print(agent);
    }

    private void Update()
    {
        if (agent != null)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }
        // playerInSightRange = Physics.CheckBox(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);





        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange || playerInAttackRange) ChasePlayer();
        // if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        print("dakhal patroling");
        SearchWalkpoint();

    }
    private void SearchWalkpoint()
    {
        print("DAKHAL YA SALMA HENA SEARCH");
        float randomZ = Random.Range(0, walkPointRange);
        float randomX = Random.Range(0, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        print(walkPoint);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

        agent.SetDestination(walkPoint);


    }

    private void ChasePlayer()
    {

        float distance = Vector3.Distance(transform.position, Player.position);
        print("EL DISTANCE" + distance);
        playerInAttackRange = distance < 2f;
        print("playerInAttackRange" + playerInAttackRange);
        if (playerInAttackRange == true)
        {
            print("EBTADA YE3mel ATTAAAAAAAACCCCCCKKKKKKK");
            agent.SetDestination(transform.position);
            transform.LookAt(Player);
            anim.SetTrigger("Attack");
        }
        else
        {
            print("DAKHAL CHASE YA SALMA");
            print(agent.SetDestination(Player.transform.position));
            agent.SetDestination(Player.transform.position);
            anim.ResetTrigger("Attack");



        }
    }


    private void AttackPlayer()
    {



        print("EBTADA YE3mel ATTAAAAAAAACCCCCCKKKKKKK");
        agent.SetDestination(transform.position);
        transform.LookAt(Player);
        anim.SetTrigger("Attack");

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }


    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        anim.ResetTrigger("Attack");

    }





}
