using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Transform mousePos;
    private Transform player;

    [SerializeField]
    private int damage;

    void Awake()
    {
        mousePos = GameObject.Find("MousePos").transform;
        player = GameObject.Find("Player").transform;
        var aim = mousePos.transform.position - player.transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(aim.normalized * 750.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

}
