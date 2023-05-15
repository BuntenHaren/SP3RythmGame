using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneSurgeEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private PlayerStats playerStats;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            textBox.SetActive(true);
            playerStats.ArcaneSurgeEnabled = true;
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
