using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [field: Header("Movement Variables")] 
    public float BaseMovementSpeed { private set; get; } = 10;
    public float BaseDashDistance { private set; get; } = 1;
    public float BaseDashCooldown { private set; get; } = 1;
    public float BaseDashDuration { private set; get; } = 0.1f;
    [field: Header("Movement Multipliers")]
    public float MovementSpeedMultiplier = 1;
    public float DashDistanceMultiplier = 1;
    public float DashCooldownMultiplier = 1;
    public float DashDurationMultiplier = 1;

    [field: Header("Attack Variables")] 
    public int BaseAttackDamage { private set; get; } = 1;
    public float BaseAttackRate { private set; get; } = 1;
    public float BaseAttackRadius { private set; get; } = 1;
    public float BaseAttackDistance { private set; get; } = 1;
    public float BasetimeForBeatWindow { private set; get; }
    public float BaseJuiceAmountOnBeat { private set; get; } = 1;
    
    [field: Header("Attack Multipliers")]
    public float AttackDamageMultiplier = 1;
    public float AttackRateMultiplier = 1;
    public float AttackRadiusMultiplier = 1;
    public float AttackDistanceMultiplier = 1;
    public float JuiceAmountOnBeatMultiplier = 1;

    [field: Header("Health Multipliers")] 
    public float HealOnAttack;
    public float HealingRecievedMultiplier = 1;
    public float DamageRecievedMultiplier = 1;
    
    public float CurrentMovementSpeed;
    public float CurrentDashDistance;
    public float CurrentDashCooldown;
    public float CurrentDashDuration;

    public int CurrentAttackDamage;
    public float CurrentAttackRate;
    public float CurrentAttackRadius;
    public float CurrentAttackDistance;
    public float CurrentTimeForBeatWindow;
    public float CurrentJuiceAmountOnBeat;

    [field: Header("Charms")]
    public Charm CurrentActiveCharm;
    public Charm CurrentPassiveCharm;
    
    public void ResetValues()
    {
        
    }
    
}
