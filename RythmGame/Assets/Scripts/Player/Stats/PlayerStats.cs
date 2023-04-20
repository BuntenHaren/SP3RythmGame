using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement Variables")]
    [SerializeField]
    private float baseMovementSpeed = 10;
    [SerializeField]
    private float baseDashDistance = 1;
    [SerializeField]
    private float baseDashCooldown = 1;
    [SerializeField]
    private float baseDashDuration = 0.1f;

    [Header("Attack Variables")] 
    [SerializeField]
    private int baseAttackDamage = 1;
    [SerializeField]
    private float baseAttackRate = 1;
    [SerializeField]
    private float baseAttackRadius = 1;
    [SerializeField]
    private float baseAttackDistance = 1;
    [SerializeField]
    private float timeForBeatWindow;
    
    
    
}
