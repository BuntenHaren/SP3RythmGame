using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject charmObject;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            charmObject.SetActive(true);
        }
    }

    
}
