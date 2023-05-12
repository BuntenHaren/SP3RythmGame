using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePop : MonoBehaviour
{
    [SerializeField]
    private GameObject messageObject;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            messageObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            messageObject.SetActive(false);
        }
    }
}
