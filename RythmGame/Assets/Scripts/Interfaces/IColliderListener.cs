using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IColliderListener
{
    public void CollisionEnter(Collision collision);
    public void TriggerEnter(Collider other);
    public void CollisionStay(Collision collision);
    public void TriggerStay(Collider other);
}
