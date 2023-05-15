using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneSurgeEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

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
