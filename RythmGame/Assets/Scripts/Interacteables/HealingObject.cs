using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int amountToHeal;
    [SerializeField]
    private int numberOfUses;
    [SerializeField]
    private Health playerHealth;

    public void TakeDamage(int amount)
    {
        if(numberOfUses <= 0)
            return;
        
        playerHealth.CurrentHealth += amountToHeal;
        numberOfUses--;
    }

    //Leave empty, this shouldn't heal itself at all!
    public void HealDamage(int amount)
    {
        
    }
}
