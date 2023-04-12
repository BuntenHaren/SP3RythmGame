using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Health healthObject;

    private bool Invurnerable;

    private void OnValidate()
    {
        if(healthObject == null)
            Debug.LogError("Health object is currently not set. Please set the Health Object to a viable object of type Health");
    }

    public void TakeDamage(int amount)
    {
        if(!Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
        }
    }

    public void HealDamage(int amount)
    {
        healthObject.CurrentHealth += amount;
    }
    
}
