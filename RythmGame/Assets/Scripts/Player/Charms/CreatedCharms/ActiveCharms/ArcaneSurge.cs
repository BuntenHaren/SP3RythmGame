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

    private JuiceBar juiceBar;

    public CharmDescriptions CharmDescription;

    public override void ActivateCharm()
    {
        if (CheckIfCanActivate() && (playerStats.ArcaneSurgeEnabled))
        {
            // (these are for some reason activated twice if there is enough juice, but since cost=max juice we don't have to worry about that now)

            base.ActivateCharm();

            // detract juice
            juiceCounter.CurrentJuice -= activationCost;

            // change stats
            playerStats.CurrentAttackDamageMultiplier *= DamageBuffMultiplier;
            playerStats.CurrentDashCooldownMultiplier *= DashCooldownMultiplier;
            playerStats.CurrentMovementSpeedMultiplier *= MoveSpeedMultiplier;
            //playerStats.CurrentAttackRateMultiplier *= AttackSpeedMultiplier;

            if(GameObject.Find("JuiceBar").GetComponent<JuiceBar>() != null)
            {
                juiceBar = GameObject.Find("JuiceBar").GetComponent<JuiceBar>();
                juiceBar.ActivateGlow();
            }
        }
    }

    protected override void EndActivation()
    {
        Debug.Log("Speed endstart: " + playerStats.CurrentMovementSpeedMultiplier);
        playerStats.CurrentAttackDamageMultiplier /= DamageBuffMultiplier;
        playerStats.CurrentDashCooldownMultiplier /= DashCooldownMultiplier;
        playerStats.CurrentMovementSpeedMultiplier /= MoveSpeedMultiplier;
        //playerStats.CurrentAttackRateMultiplier /= AttackSpeedMultiplier;
        Debug.Log("Speed endend: " + playerStats.CurrentMovementSpeedMultiplier);

        if (juiceBar != null)
            juiceBar.RemoveGlow();
    }
}