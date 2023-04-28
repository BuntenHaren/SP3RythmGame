using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "RythmGame/Boss/BossStats")]
public class BossStats : ScriptableObject
{
    [field: Header("Circle Sector Attack Variables")]
    public int AmountOfSlices;
    public float Range;
    public float SectorAngle;
    public float CircleDamage;

    [field: Header("Hoof Stomp Attack Variables")]
    public float StompDamage;
    public int NumberOfStompsTotal;
    public int NumberOfBeatsWarningForStomp;
    public float StompRadius;
    
    [field: Header("Idle State Variables")]
    public float IdleTimeRangeMax;
    public float IdleTimeRangeMin;
    
    
    [field: Header("Thresholds")]
    public float ThresholdForEnrage;
    
    [field: Header("Health Multipliers")]
    public float HealingRecievedMultiplier = 1;
    public float DamageRecievedMultiplier = 1;
}
