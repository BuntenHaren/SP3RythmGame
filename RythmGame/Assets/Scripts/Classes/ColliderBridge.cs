using System;
using Bosses;
using UnityEngine;
using Unity;

public class ColliderBridge : MonoBehaviour
{
    [SerializeField]
    private BossBehaviour _listener;

    private void OnCollisionStay(Collision collision)
    {
        _listener.OnCollisionStay(collision);
    }

    private void OnTriggerStay(Collider other)
    {
        _listener.OnTriggerStay(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        _listener.OnCollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        _listener.OnTriggerEnter(other);
    }
}