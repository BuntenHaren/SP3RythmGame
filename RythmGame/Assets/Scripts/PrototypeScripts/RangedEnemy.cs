using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float distanceToStop;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > distanceToStop)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
        }
    }

    public void ShootProjectile()
    {
        Instantiate(projectile, gameObject.GetComponent<Transform>());
    }
}
