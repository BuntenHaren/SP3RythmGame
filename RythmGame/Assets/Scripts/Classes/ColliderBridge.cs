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
        _listener.CollisionStay(collision, gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        _listener.TriggerStay(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        _listener.CollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        _listener.TriggerEnter(other);
    }
}