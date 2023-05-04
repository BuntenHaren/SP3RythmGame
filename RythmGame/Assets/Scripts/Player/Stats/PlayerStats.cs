using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "RythmGame/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [field: Header("Movement Variables")] 
    public float BaseMovementSpeed { private set; get; } = 10;
    public float BaseDashDistance { private set; get; } = 1;
    public float BaseDashCooldown { private set; get; } = 1;
    public float BaseDashDuration { private set; get; } = 0.1f;
    
    [field: Header("Movement Multipliers")]
    public float BaseMovementSpeedMultiplier = 1;
    public float BaseDashDistanceMultiplier = 1;
    public float BaseDashCooldownMultiplier = 1;
    public float BaseDashDurationMultiplier = 1;

    [field: Header("Attack Variables")] 
    public int BaseAttackDamage { private set; get; } = 1;
    public float BaseAttackRate { private set; get; } = 1;
    public float BaseAttackRadius { private set; get; } = 1;
    public float BaseAttackDistance { private set; get; } = 1;
    public float BasetimeForBeatWindow { private set; get; }
    public float BaseJuiceAmountOnBeat { private set; get; } = 1;
    
    [field: Header("Attack Multipliers")]
    public float BaseAttackDamageMultiplier = 1;
    public float BaseAttackRateMultiplier = 1;
    public float BaseAttackRadiusMultiplier = 1;
    public float BaseAttackDistanceMultiplier = 1;
    public float BaseJuiceAmountOnBeatMultiplier = 1;

    [field: Header("Health Multipliers")] 
    public float BaseHealOnAttack;
    public float BaseHealOnAttackMultiplier;
    public float BaseHealingRecievedMultiplier = 1;
    public float BaseDamageRecievedMultiplier = 1;
    public float BaseMaxHealthMultiplier = 1;
    
    [HideInInspector]
    public float CurrentMovementSpeed, 
        CurrentDashDistance, 
        CurrentDashCooldown, 
        CurrentDashDuration;

    [HideInInspector]
    public int CurrentAttackDamage;

    [HideInInspector] public float CurrentAttackRate,
        CurrentAttackRadius,
        CurrentAttackDistance,
        CurrentTimeForBeatWindow,
        CurrentJuiceAmountOnBeat,
        CurrentHealOnAttack,
        CurrentAttackDamageMultiplier,
        CurrentAttackRateMultiplier,
        CurrentAttackRadiusMultiplier,
        CurrentAttackDistanceMultiplier,
        CurrentJuiceAmountOnBeatMultiplier,
        CurrentMovementSpeedMultiplier,
        CurrentDashDistanceMultiplier,
        CurrentDashCooldownMultiplier,
        CurrentDashDurationMultiplier,
        CurrentHealOnAttackMultiplier,
        CurrentMaxHealthMultiplier;

    [field: Header("Charms")]
    public Charm CurrentActiveCharm;
    public Charm CurrentPassiveCharm;
    
    public void ResetValues()
    {
        ResetCurrentVariables();
        ResetMultiplierVariables();
    }

    private void ResetCurrentVariables()
    {
        CurrentMovementSpeed = BaseMovementSpeed;
        CurrentDashCooldown = BaseDashCooldown;
        CurrentDashDistance = BaseDashDistance;
        CurrentDashDuration = BaseDashDuration; 
        CurrentAttackDamage = BaseAttackDamage;
        CurrentAttackRate = BaseAttackRate;
        CurrentAttackRadius = BaseAttackRadius;
        CurrentAttackDistance = BaseAttackDistance;
        CurrentTimeForBeatWindow = BasetimeForBeatWindow;
        CurrentJuiceAmountOnBeat = BaseJuiceAmountOnBeat;
        CurrentHealOnAttack = BaseHealOnAttack;
    }

    private void ResetMultiplierVariables()
    {
        CurrentAttackDamageMultiplier = BaseAttackDamageMultiplier;
        CurrentAttackRateMultiplier = BaseAttackRateMultiplier;
        CurrentAttackRadiusMultiplier = BaseAttackRadiusMultiplier;
        CurrentAttackDistanceMultiplier = BaseAttackDistanceMultiplier;
        CurrentJuiceAmountOnBeatMultiplier = BaseJuiceAmountOnBeatMultiplier;
        CurrentMovementSpeedMultiplier = BaseMovementSpeedMultiplier;
        CurrentDashDistanceMultiplier = BaseDashDistanceMultiplier;
        CurrentDashCooldownMultiplier = BaseDashCooldownMultiplier;
        CurrentDashDurationMultiplier = BaseDashDurationMultiplier;
        CurrentHealOnAttackMultiplier = BaseHealOnAttackMultiplier;
        CurrentMaxHealthMultiplier = BaseMaxHealthMultiplier;
    }

}
