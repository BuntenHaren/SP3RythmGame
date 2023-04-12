using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dashingSpeed;
    [SerializeField]
    private float distanceToStop;
    private float walkingSpeed;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private MeleeAoeAttack attackScript;
    [SerializeField]
    private float attackCD;
    private float timeSinceLastAttack;
    public bool attacking = false;
    public bool inAttackRange = false;

    [SerializeField]
    private float dashForce;
    [SerializeField]
    private float dashDuration;

    [SerializeField]
    private BeatManager beatManager;

    void Awake()
    {
        walkingSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (beatManager.dashing == true)
        {
            speed = dashingSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }
        timeSinceLastAttack += Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
        }
    }

    public void StartAttack()
    {
        if(timeSinceLastAttack > attackCD && inAttackRange)
        {
            attacking = true;
            timeSinceLastAttack = 0f;
            attackScript.ExecuteAttack();
        }
    }
}
