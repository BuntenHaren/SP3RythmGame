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
}
