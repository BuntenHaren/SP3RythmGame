using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource beatSound;

    public int beatsPerMinute;
    private float beatCounter;
    private float beatInterval;
    private int numberOfBeats;
    public bool onBeat;

    public bool dashing = false;

    [SerializeField]
    private GameObject[] meleeEnemies;

    [SerializeField]
    private GameObject[] rangedEnemies;

    void Awake()
    {
        beatCounter = 100f;
        beatInterval = 1f / (beatsPerMinute / 60f);
    }

    void Update()
    {
        beatCounter += Time.deltaTime;
        if(beatCounter >= beatInterval-0.1f)
        {
            dashing = true;
        }
        if(beatCounter >= beatInterval)
        {
            dashing = false;
            beatCounter = 0f;
            numberOfBeats++;
            onBeat = true;
            beatSound.Play();
            for (int i = 0; i < meleeEnemies.Length; i++)
            {
                meleeEnemies[i].GetComponent<MeleeEnemy>().StartAttack();
            }
            if (numberOfBeats % 2 == 1)
            {
                for (int i = 0; i < rangedEnemies.Length; i++)
                {
                    rangedEnemies[i].GetComponent<RangedEnemy>().ShootProjectile();
                }
            }
        }
    }
}
