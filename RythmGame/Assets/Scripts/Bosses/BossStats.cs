using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Boss/BossStats")]
public class BossStats : ScriptableObject
{
    [field: Header("Circle Sector Attack Variables")]
    public int AmountOfSlices;
    public float Radius;
    public float SectorAngle;
    public float CircleDamage;

    [field: Header("Hoof Stomp Attack Variables")]
    public float StompDamage;
    
    
    [field: Header("Thresholds")]
    public float ThresholdForEnrage;
    
    [field: Header("Health Multipliers")]
    public float HealingRecievedMultiplier = 1;
    public float DamageRecievedMultiplier = 1;
}
