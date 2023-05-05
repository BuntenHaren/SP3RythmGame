using System;
using UnityEngine;
using Unity;

public class ColliderBridge : MonoBehaviour
{
    IColliderListener _listener;
    public void Initialize(IColliderListener l)
    {
        _listener = l;
    }

    private void OnCollisionStay(Collision collision)
    {
        _listener.OnCollisionStay(collision);
    }

    private void OnTriggerStay(Collider other)
    {
        _listener.OnTriggerStay(other);
    }
}