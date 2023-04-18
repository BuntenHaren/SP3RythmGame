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
    [SerializeField]
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
    [SerializeField]
    private PHealth playerHealth;
    [SerializeField]
    private int damageAmount;
    private bool attacking = false;
    [SerializeField]
    private float minimumAttackWindUp;
    private float attackTimer = 0f;

    void Start()
    {
        eventPort.onBeat += Attack;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (rotationPivot != null && !attacking)
        {
            rotationPivot.rotation = Quaternion.LookRotation(rotationPivot.position - player.position);
        }
    }

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
            Debug.Log("executeAttackSucess");
            sr.DOColor(originalColor, 0.4f).SetEase(Ease.InBack);
            if (playerInDamageArea)
            {
                //playerHealth.TakeDamage(damageAmount);
            }
            attacking = false;
            enemyScript.stopAttack();
        }
    }

    public void StartTelegraph()
    {
        Debug.Log("StartTelegraph");
        attackTimer = 0f;
        attacking = true;
        sr.DOColor(windUpColor, 0.4f).SetEase(Ease.OutSine);
    }
}
