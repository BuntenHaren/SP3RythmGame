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
    public bool engaged;
    private Rigidbody rb;

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

    //Animation
    [SerializeField]
    private Animator anim;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        eventPort.onBeat += Attack;
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        timeSinceAttack += Time.deltaTime;
        if(engaged)
        {
            Move();
        }
    }

    private void Attack()
    {
        if (distanceToPlayer < attackRange && timeSinceAttack > attackCD && !attacking)
        {
            anim.SetBool("Telegraphing", true);
            anim.SetBool("Moving", false);
            attacking = true;
            var randomNumber = Random.Range(0, 2);
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
        if (transform.position.x > player.position.x)
        {
            anim.SetBool("FacingLeft", true);
        }
        else
        {
            anim.SetBool("FacingLeft", false);
        }

        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop && !attacking)
        {
            anim.SetBool("Moving", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    private void ConeAttack()
    {
        coneAttackObject.GetComponent<EnemyMeleeAttack>().StartTelegraph();

    }
    private void CircleAttack()
    {
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
