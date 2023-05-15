using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMasterEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private PassiveCharmIcon passiveCharmIcon;
    [SerializeField]
    private PlayerStats playerStats;

    private bool activated = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !activated)
        {
            textBox.SetActive(true);
            playerStats.BeatMasterEnabled = true;
            passiveCharmIcon.ChangeIcon(passiveCharmIcon.BeatMasterIcon);
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
