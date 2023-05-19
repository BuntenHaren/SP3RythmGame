using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "RythmGame/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [field: Header("Movement Variables")] 
    public float BaseMovementSpeed = 10;
    public float BaseDashDistance = 1;
    public float BaseDashCooldown = 1;
    public float BaseDashDuration = 0.1f;
    
    [field: Header("Movement Multipliers")]
    public float BaseMovementSpeedMultiplier = 1;
    public float BaseDashDistanceMultiplier = 1;
    public float BaseDashCooldownMultiplier = 1;
    public float BaseDashDurationMultiplier = 1;

    [field: Header("Attack Variables")] 
    public int BaseAttackDamage = 1;
    public float BaseAttackRate = 1;
    public float BaseAttackRadius = 1;
    public float BaseAttackDistance = 1;
    public float BasetimeForBeatWindow;
    public float BaseJuiceAmountOnBeat = 1;
    public float BaseForceTowardAttack = 1;
    
    [field: Header("Attack Multipliers")]
    public float BaseAttackDamageMultiplier = 1;
    public float BaseAttackRateMultiplier = 1;
    public float BaseAttackRadiusMultiplier = 1;
    public float BaseAttackDistanceMultiplier = 1;
    public float BaseJuiceAmountOnBeatMultiplier = 1;

    [field: Header("Health Multipliers")] 
    public float BaseHealOnAttack;
    public float BaseHealOnAttackMultiplier = 1;
    public float BaseHealingRecievedMultiplier = 1;
    public float BaseDamageRecievedMultiplier = 1;
    public float BaseMaxHealthMultiplier = 1;
    
    [field: Header("Charms")]
    public ActiveCharm CurrentActiveCharm;
    public PassiveCharm CurrentPassiveCharm;
    public bool ActiveCharmActivated,
        BeatMasterEnabled,
        ArcaneGorgerEnabled,
        ArcaneSurgeEnabled;

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

        // charms
        BeatMasterEnabled = false;
        ArcaneGorgerEnabled = false;
        ArcaneSurgeEnabled = false;
        ActiveCharmActivated = false;
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
