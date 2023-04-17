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
    private float timeSinceAttack;
    private bool inAttackRange = false;
    private bool attacking = false;

    void Start()
    {
        eventPort.onBeat += Attack;
    }

    void FixedUpdate()
    {
        timeSinceAttack += Time.deltaTime;
        Move();
    }

    private void Attack()
    {
        Debug.Log("Attack");
        var randomNumber = Random.Range(0, 1);
        if (inAttackRange && timeSinceAttack > attackCD)
        {
            attacking = true;
            timeSinceAttack = 0f;

            if (randomNumber == 0)
            {
                StartCoroutine(ConeAttack());
            }
            else
            {
                StartCoroutine(CircleAttack());
            }
        }
    }

    private void Move()
    {
        //Fix navmesh pathfinding when obstacles are introduced
        if(Vector3.Dot(player.position, transform.position) < 0)
        {
            Debug.Log("Left");
        }
        else
        {
            Debug.Log("Right");
        }
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        }
    }

    private IEnumerator ConeAttack()
    {
        Debug.Log("ConeAttack");
        //Animator.SetBool("ConeAttack", true);
        coneAttackObject.GetComponent<EnemyMeleeAttack>().StartTelegraph();
        yield return new WaitForSeconds(coneAttackWindUp);
        coneAttackObject.GetComponent<EnemyMeleeAttack>().ExecuteAttack();
        yield return new WaitForSeconds(coneAttackRecoverTime);
        //Animator.SetBool("ConeAttack", false);
        attacking = false;

    }
    private IEnumerator CircleAttack()
    {
        Debug.Log("CircleAttack");
        //Animator.SetBool("CircleAttack", true);
        circleAttackObject.GetComponent<EnemyMeleeAttack>().StartTelegraph();
        yield return new WaitForSeconds(circleAttackWindUp);
        circleAttackObject.GetComponent<EnemyMeleeAttack>().ExecuteAttack();
        yield return new WaitForSeconds(circleAttackRecoverTime);
        //Animator.SetBool("CircleAttack", false);
        attacking = false;
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
}
