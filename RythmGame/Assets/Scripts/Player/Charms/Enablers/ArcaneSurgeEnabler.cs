using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneSurgeEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private ActiveCharm arcaneSurge;

    [SerializeField]
    private ActiveCharmIcon activeCharmIcon;
    [SerializeField]
    private PlayerStats playerStats;

    private bool activated = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textBox.SetActive(true);
            playerStats.ArcaneSurgeEnabled = true;
            if(!activated)
                activeCharmIcon.ChangeIcon(activeCharmIcon.ArcaneSurgeIcon);
            activated = true;

            if (playerStats.CurrentActiveCharm != arcaneSurge)
            {
                playerStats.CurrentActiveCharm = arcaneSurge;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }
}
