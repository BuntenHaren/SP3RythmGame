using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMasterEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private PassiveCharm beatMaster;

    [SerializeField]
    private PassiveCharmIcon passiveCharmIcon;
    [SerializeField]
    private PlayerStats playerStats;

    private bool activated = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textBox.SetActive(true);
            playerStats.BeatMasterEnabled = true;
            if(!activated)
                passiveCharmIcon.ChangeIcon(passiveCharmIcon.BeatMasterIcon);
            activated = true;

            if (playerStats.CurrentPassiveCharm != beatMaster)
            {
                Debug.Log("Switching charm from " + playerStats.CurrentPassiveCharm + " to " + beatMaster);
                playerStats.CurrentPassiveCharm.Finish();
                playerStats.CurrentPassiveCharm = beatMaster;
                playerStats.CurrentPassiveCharm.Start();
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
