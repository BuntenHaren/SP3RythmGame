using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/ArcaneSurge")]
public class ArcaneSurge : ActiveCharm
{
    private float DamageBuffMultiplier = 1f;
    private float DashCooldownMultiplier = 0.5f;
    private float MoveSpeedMultiplier = 2f;
    private float AttackSpeedMultiplier = 2f;


    public override void ActivateCharm()
    {
        Debug.Log("arcanesurge activated");
        if (CheckIfCanActivate())
        {
            Debug.Log("can be activated");
            base.ActivateCharm();

            // detract juice
            juiceCounter.CurrentJuice -= activationCost;

            // change stats
            playerStats.CurrentAttackDamageMultiplier *= DamageBuffMultiplier;
            playerStats.CurrentDashCooldownMultiplier *= DashCooldownMultiplier;
            playerStats.CurrentMovementSpeedMultiplier *= MoveSpeedMultiplier;
            playerStats.CurrentAttackRateMultiplier *= AttackSpeedMultiplier;
        }
    }

    protected override void EndActivation()
    {
        Debug.Log("Arcane surge ended");
        playerStats.CurrentAttackDamageMultiplier /= DamageBuffMultiplier;
        playerStats.CurrentDashCooldownMultiplier /= DashCooldownMultiplier;
        playerStats.CurrentMovementSpeedMultiplier /= MoveSpeedMultiplier;
        playerStats.CurrentAttackRateMultiplier /= AttackSpeedMultiplier;
    }
}

/*
[CreateAssetMenu(menuName = "RythmGame/Player/ActiveCharm")]
public abstract class ActiveCharm : Charm
{
    [SerializeField]
    protected float activationCost;
    [SerializeField]
    protected float activeDuration;

    protected Timer activationTimer;

    public override void Start()
    {
        activationTimer = new Timer();
        base.Start();
    }

    public virtual void ActivateCharm()
    {
        activationTimer.StartTimer(activeDuration);
        //Insert your SFX below this comment for the charm activation and probably start playing the active SFX as well :)

    }

    protected virtual bool CheckIfCanActivate()
    {
        return activationCost >= juiceCounter.CurrentJuice;
    }

    public override void Update()
    {
        activationTimer.UpdateTimer(Time.deltaTime);
    }

    protected virtual void EndActivation()
    {

    }

}
*/