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
        if(chargesLeft <= 0)
            return;

        playerHealth.CurrentHealth += healingPerCharge;
        chargesLeft--;
    }

    public void TakeDamageOnBeat(float amount)
    {
        if(chargesLeft <= 0)
            return;

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
            GetComponent<SpriteRenderer>().sprite = sp0;
        Debug.Log("0sprite");
        if (chargesLeft == 1)
            GetComponent<SpriteRenderer>().sprite = sp1;
             Debug.Log("0sprite");
        if (chargesLeft == 2)
            GetComponent<SpriteRenderer>().sprite = sp2;
        if (chargesLeft == 3)
            GetComponent<SpriteRenderer>().sprite = sp3;
    }


}
