using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;

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
    public bool attackQueued = false;

    //Attack
    [SerializeField]
    private PHealth playerHealth;
    [SerializeField]
    private int damageAmount;
    private bool attacking = false;

    void Start()
    {

    }

    void Update()
    {
        if(rotationPivot != null && !attacking)
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

    public void ExecuteAttack()
    {
        sr.DOColor(executeColor, 0.1f).SetEase(Ease.OutElastic).OnComplete(FadeOutTelegraph);
        if (attackQueued && playerInDamageArea)
        {
            playerHealth.TakeDamage(damageAmount);
        }
        attacking = false;
    }

    public void StartTelegraph()
    {
        attacking = true;
        sr.DOColor(windUpColor, 0.4f).SetEase(Ease.OutSine);
    }

    private void FadeOutTelegraph()
    {
        sr.DOColor(originalColor, 0.2f).SetEase(Ease.InOutSine);
    }
}
