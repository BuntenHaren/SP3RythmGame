using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform player;
    // Update is called once per frame
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        Vector3 shoot = (player.position - transform.position).normalized;
        gameObject.GetComponent<Rigidbody>().AddForce(shoot * 580.0f);
    }
}
