using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/ArcaneSurge")]
public class ArcaneSurge : ActiveCharm
{
    [SerializeField]
    private float DamageBuffMultiplier = 1.5f;
    [SerializeField]
    private float MoveSpeedMultiplier = 1.4f;

    private JuiceBar juiceBar;



    public override void ActivateCharm()
    {
        Debug.Log("Can activate: " + CheckIfCanActivate());
        Debug.Log("Juice: " + juiceCounter.CurrentJuice);
        if (CheckIfCanActivate())
        {
            // (these are for some reason activated twice if there is enough juice, but since cost=max juice we don't have to worry about that now)
            Debug.Log("active activated");
            activationTimer.StartTimer(activeDuration);
            playerStats.ActiveCharmActivated = true;

            // detract juice
            juiceCounter.CurrentJuice -= activationCost;
            Debug.Log("activated 3");

            // change stats
            playerStats.CurrentAttackDamageMultiplier *= DamageBuffMultiplier;
            playerStats.CurrentMovementSpeedMultiplier *= MoveSpeedMultiplier;

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
        playerStats.CurrentMovementSpeedMultiplier /= MoveSpeedMultiplier;
        Debug.Log("Speed endend: " + playerStats.CurrentMovementSpeedMultiplier);

        playerStats.ActiveCharmActivated = false;
        if (juiceBar != null)
            juiceBar.RemoveGlow();
    }
}