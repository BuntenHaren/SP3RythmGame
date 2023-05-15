using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Player/Arcane Gorger")]
public class ArcaneGorger : PassiveCharm
{
    private float JuiceLevelRatio = 0f;
    private float BeatAccuracy = 0f;

    private float BeatAccuracyMultiplier = 1f;
    private float BaseHeal = 10.0f;
    private float JuiceLevelMultiplier = 1f;
    private float MaxHealthMultiplier = 0.75f;

    private float DistanceToLastBeat = 0f;
    private float DistanceToNextBeat = 0f;
    private float BeatWindow;

    public override void Start()
    {
        base.Start();

        // get beat window 
        BeatWindow = playerStats.CurrentTimeForBeatWindow;

        // multiply health
        Debug.Log("----- ARCANE GORGER START -----");
        Debug.Log("Current health before reduction: " + playerHealth.CurrentHealth);
        Debug.Log("Max health before reduction: " + playerHealth.CurrentMaxHealth);
        Debug.Log("Max health multiplier before reduction: " + playerStats.CurrentMaxHealthMultiplier);

        playerHealth.CurrentHealth *= MaxHealthMultiplier;
        playerStats.CurrentMaxHealthMultiplier *= MaxHealthMultiplier;

        Debug.Log("Current health after reduction: " + playerHealth.CurrentHealth);
        Debug.Log("Max health after reduction: " + playerHealth.CurrentMaxHealth);
        Debug.Log("Max health multiplier after reduction: " + playerStats.CurrentMaxHealthMultiplier);
        Debug.Log("------------------------------");


        /* test
        Debug.Log("Initial multiplier: " + playerHealth.CurrentMaxHealth);
        playerStats.CurrentMaxHealthMultiplier = 3f;
        Debug.Log("multiplier = 3f: " + playerHealth.CurrentMaxHealth);
        playerStats.CurrentMaxHealthMultiplier *= 0.5f;
        Debug.Log("multiplier *= 0.5f: " + playerHealth.CurrentMaxHealth);
        playerStats.CurrentMaxHealthMultiplier *= 0.5f;
        Debug.Log("multiplier *= 0.5f: " + playerHealth.CurrentMaxHealth);
        playerStats.CurrentMaxHealthMultiplier *= 6f;
        Debug.Log("multiplier = 6f: " + playerHealth.CurrentMaxHealth);*/
    }

    public override void OnPlayerAttackAction()
    {
        base.OnPlayerAttackAction();

        // calculate juice gauge fill ratio
        JuiceLevelRatio = juiceCounter.CurrentJuice / juiceCounter.MaxJuice;

        DistanceToLastBeat = beatPort.GetDistanceToLastBeat();
        DistanceToNextBeat = beatPort.GetDistanceToNextBeat();

        
        // set BeatAccuracy to 1 of on beat, else 0
        if ((DistanceToLastBeat <= BeatWindow) || (DistanceToNextBeat <= BeatWindow))
        {
            BeatAccuracy = BeatAccuracyMultiplier;
        }
        else
        {
            BeatAccuracy = 0f;
        }

        // set healing
        playerStats.CurrentHealOnAttack = BaseHeal * BeatAccuracy * JuiceLevelRatio;

        Debug.Log("Base heal: " + BaseHeal +
            "\nBeat accuracy: " + BeatAccuracy + 
            "\nCurrent juice: " + juiceCounter.CurrentJuice + 
            "\nJuice ratio: " + JuiceLevelRatio + 
            "\nJuice multiplier: " + JuiceLevelMultiplier + 
            "\nHeal amount: " + playerStats.CurrentHealOnAttack + 
            "\n");
    }

    public override void Finish()
    {
        base.Finish();
        Debug.Log("---- ARCANE GORGER FINISH: ----");
        // reset health
        Debug.Log("Max health before restoration: " + playerHealth.CurrentMaxHealth);
        Debug.Log("Current health before restoration: " + playerHealth.CurrentHealth);
        Debug.Log("Max health multiplier before restoration: " + playerStats.CurrentMaxHealthMultiplier);

        playerStats.CurrentHealOnAttack = 0f;
        playerStats.CurrentMaxHealthMultiplier = 1f;
        playerHealth.CurrentHealth /= MaxHealthMultiplier;

        Debug.Log("Current health before restoration: " + playerHealth.CurrentHealth);
        Debug.Log("Max health after restoration: " + playerHealth.CurrentMaxHealth);
        Debug.Log("Max health multiplier after restoration: " + playerStats.CurrentMaxHealthMultiplier);
        Debug.Log("------------------------------");
    }
}
