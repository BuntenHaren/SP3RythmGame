using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;

    //Movement
    private Transform player;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveAwayFromEnemySpeed;
    [SerializeField]
    private float distanceToStop;
    private float distanceToPlayer;
    [HideInInspector]
    public Transform closestEnemy;

    //Health
    private int health;
    [SerializeField]
    private int maxHealth;

    //Attack
    [SerializeField]
    private GameObject coneAttackObject;
    [SerializeField]
    private GameObject circleAttackObject;
    [SerializeField]
    private float attackCD;
    [SerializeField]
    private float attackRange;
    private float timeSinceAttack = 5f;
    private bool attacking = false;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        eventPort.onBeat += Attack;
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        timeSinceAttack += Time.deltaTime;
        Move();
    }

    private void Attack()
    {
        var randomNumber = Random.Range(0, 2);
        if (distanceToPlayer < attackRange && timeSinceAttack > attackCD && !attacking)
        {
            attacking = true;
            if (randomNumber == 1)
            {
                ConeAttack();
            }
            else
            {
                CircleAttack();
            }
        }
    }

    private void Move()
    {
        //Fix navmesh pathfinding when obstacles are introduced
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        }
        if(closestEnemy != null && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestEnemy.position, -1 * moveAwayFromEnemySpeed);
        }
    }

    private void ConeAttack()
    {
        //Animator.SetBool("ConeAttack", true);
        coneAttackObject.GetComponent<EnemyMeleeAttack>().StartTelegraph();

    }
    private void CircleAttack()
    {
        //Animator.SetBool("CircleAttack", true);
        circleAttackObject.GetComponent<EnemyMeleeAttack>().StartTelegraph();
    }

    public void stopAttack()
    {
        attacking = false;
        timeSinceAttack = 0f;
        //Animator.SetBool("CircleAttack", false);
        //Animator.SetBool("ConeAttack", false);
    }
}
