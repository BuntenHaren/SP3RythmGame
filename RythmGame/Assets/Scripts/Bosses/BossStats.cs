using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : ScriptableObject
{
    [field: Header("Movement Variables")] 
    public float BaseMovementSpeed { private set; get; } = 10;
        
    [field: Header("Movement Multipliers")]
    public float MovementSpeedMultiplier = 1;

    [field: Header("Health Multipliers")]
    public float HealingRecievedMultiplier = 1;
    public float DamageRecievedMultiplier = 1;
    
    public float CurrentMovementSpeed;

    public int CurrentAttackDamage;
    public float CurrentAttackRate;
}
