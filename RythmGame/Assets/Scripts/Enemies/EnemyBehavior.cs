using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehavior : MonoBehaviour
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

    //Attack
    [SerializeField]
    private GameObject coneAttackObject;
    [SerializeField]
    private float coneAttackDuration;
    [SerializeField]
    private GameObject circleAttackObject;
    [SerializeField]
    private float circleAttackDuration;
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
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        }
    }

    private IEnumerator ConeAttack()
    {
        //Animator.SetBool("ConeAttack", true);
        coneAttackObject.SetActive(true);
        yield return new WaitForSeconds(coneAttackDuration);
    }
    private IEnumerator CircleAttack()
    {
        //Animator.SetBool("CircleAttack", true);
        circleAttackObject.SetActive(true);
        yield return new WaitForSeconds(circleAttackDuration);
    }
}
