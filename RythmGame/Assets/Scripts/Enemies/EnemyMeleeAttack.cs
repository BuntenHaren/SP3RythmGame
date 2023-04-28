using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;
    [SerializeField]
    private EnemyBehavior enemyScript;

    //Rotation
    private Transform player;
    [SerializeField]
    private Transform rotationPivot;

    //Sprite Color
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private Color windUpColor;
    [SerializeField]
    private Color executeColor;

    private bool playerInDamageArea = false;

    //Attack
    private PlayerHealth playerHealth;
    [SerializeField]
    private int damageAmount;
    private bool attacking = false;
    [SerializeField]
    private float minimumAttackWindUp;
    private float attackTimer = 0f;
    [SerializeField]
    private float attackHoldWaitTime;

    //Parent Animator
    [SerializeField]
    private Animator anim;

    //Sound
    [SerializeField]
    public EventReference WerewolfAttackExecuteSound;
    [SerializeField]
    public EventReference WerewolfAttackStartSound;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        eventPort.onBeat += Attack;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    //Check if player is in damage area
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            playerInDamageArea = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerInDamageArea = false;
        }
    }

    public void Attack()
    {
        if(attacking && attackTimer > minimumAttackWindUp)
        {
            //Execute attack sound here
            anim.SetBool("ExecuteAttack", true);
            anim.SetBool("AttackHold", false);
            anim.SetBool("SwipeAttack", false);
            anim.SetBool("CircleAttack", false);
            sr.DOColor(originalColor, 0.4f).SetEase(Ease.InBack);
            if (playerInDamageArea)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            attacking = false;
            enemyScript.stopAttack();
        }
    }

    public void StartTelegraph()
    {
        //Start of telegraph sound here
        if(rotationPivot != null)
        {
            rotationPivot.rotation = Quaternion.LookRotation(rotationPivot.position - player.position);
        }
        attackTimer = 0f;
        attacking = true;
        sr.DOColor(windUpColor, 0.4f).SetEase(Ease.OutSine);
        StartCoroutine(AttackHoldAnimation(attackHoldWaitTime));
    }

    private IEnumerator AttackHoldAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("AttackHold", true);
    }
}
