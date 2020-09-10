using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : PhysicalObject
{
    public float velocity;

    private Transform player;
    private float starRadius;

    protected override void Start()
    {
        starRadius = Random.Range(1.5f, 4f);
        transform.GetChild(0).localScale = Vector3.one * starRadius * 2;
        GetComponent<SphereCollider>().radius = starRadius;

        base.Start();

        player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rb.AddForce(-direction * mass * velocity, ForceMode.Impulse);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void LoopAround()
    {
        if (transform.position.x - 10f > Const.BOUNDARY_X)
        {
            Die();
        }
        else if (transform.position.x + 10f < -Const.BOUNDARY_X)
        {
            Die();
        }

        if (transform.position.y - 10f > Const.BOUNDARY_Y)
        {
            Die();
        }
        else if (transform.position.y + 10f < -Const.BOUNDARY_Y)
        {
            Die();
        }
    }

    private void Die()
    {
        StarFactory.singleton.starCount -= 1;
        Destroy(gameObject);
    }
}
