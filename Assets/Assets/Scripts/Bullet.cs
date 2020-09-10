using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        GetComponent<Rigidbody>().velocity = player.transform.forward * player.GetComponent<Player>().bulletVelocity +  
            (-player.GetComponent<Rigidbody>().velocity.magnitude * player.transform.forward);
        Invoke("Die", 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet"))
            Invoke("Die", 0.1f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
