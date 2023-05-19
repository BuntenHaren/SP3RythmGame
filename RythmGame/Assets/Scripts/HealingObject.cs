using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealingObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private int amountOfCharges;
    [SerializeField]
    private float healingPerCharge;
    
    private int chargesLeft;

    
    public Sprite sp0, sp1, sp2, sp3;

    private void Start()
    {
        chargesLeft = amountOfCharges;
    }

    public void TakeDamage(float amount)
    {
        GetHit();
    }

    public void TakeDamageOnBeat(float amount)
    {   
        GetHit();
    }

    private void GetHit()
    {
        if(chargesLeft <= 0)
        {
            enabled = false;
            return;
        }
        
        GetComponentInParent<Animator>().SetTrigger("Heal");
        GameObject.FindWithTag("Player").GetComponentInChildren<Animator>().SetTrigger("Heal");
        playerHealth.CurrentHealth += healingPerCharge;
        chargesLeft--;
    }

    public void HealDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (chargesLeft == 0)
        {
            GetComponent<SpriteRenderer>().sprite = sp0;
            GetComponent<BoxCollider>().enabled = false;
        }
        if (chargesLeft == 1)
            GetComponent<SpriteRenderer>().sprite = sp1;
        if (chargesLeft == 2)
            GetComponent<SpriteRenderer>().sprite = sp2;
        if (chargesLeft == 3)
            GetComponent<SpriteRenderer>().sprite = sp3;
    }


}
