using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IColliderListener
{
    public void OnCollisionEnter(Collision collision);
    public void OnTriggerEnter(Collider other);
    public void OnCollisionStay(Collision collision);
    public void OnTriggerStay(Collider other);
}
