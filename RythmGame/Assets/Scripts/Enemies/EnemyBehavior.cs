using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{
    [SerializeField]
    private MusicEventPort eventPort;

    //Movement
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float distanceToStop;
    private float distanceToPlayer;


    //Health
    private int health;
    [SerializeField]
    private int maxHealth;

    //Attack
    [SerializeField]
    private GameObject coneAttackObject;
    [SerializeField]
    private float coneAttackWindUp;
    [SerializeField]
    private float coneAttackRecoverTime;
    [SerializeField]
    private GameObject circleAttackObject;
    [SerializeField]
    private float circleAttackWindUp;
    [SerializeField]
    private float circleAttackRecoverTime;
    [SerializeField]
    private float attackCD;
    [SerializeField]
    private float attackRange;
    private float timeSinceAttack = 0f;
    private bool attacking = false;


    void Start()
    {
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

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if (health <= 0)
        {
            Debug.Log("Dead");
            //Die
            //Animator.SetBool("IsDead", true)
        }
    }

    public void HealDamage(int damage)
    {
        health = health + damage;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void stopAttack()
    {
        attacking = false;
        timeSinceAttack = 0f;
        //Animator.SetBool("CircleAttack", false);
        //Animator.SetBool("ConeAttack", false);
    }
}
