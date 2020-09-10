using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    protected float mass;
    protected float radius;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        Vector3 dimensions = transform.GetChild(0).localScale;
        mass = dimensions.x * dimensions.y * dimensions.z;


        //mass is equal to volume in this case (density is 1?)
        radius = Mathf.Pow(0.75f * mass / Mathf.PI, 1f / 3f);

        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
    }

    protected virtual void FixedUpdate()
    {
        LoopAround();
        Gravity();
    }

    protected virtual void LoopAround()
    {
        if(transform.position.x > Const.BOUNDARY_X)
        {
            transform.position = new Vector3(-Const.BOUNDARY_X, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -Const.BOUNDARY_X)
        {
            transform.position = new Vector3(Const.BOUNDARY_X, transform.position.y, transform.position.z);
        }

        if (transform.position.y > Const.BOUNDARY_Y)
        {
            transform.position = new Vector3(transform.position.x, -Const.BOUNDARY_Y, transform.position.z);
        }
        else if (transform.position.y < -Const.BOUNDARY_Y)
        {
            transform.position = new Vector3(transform.position.x, Const.BOUNDARY_Y, transform.position.z);
        }
    }

    protected void Gravity()
    {
        PhysicalObject[] physicalObjects = FindObjectsOfType(typeof(PhysicalObject)) as PhysicalObject[];

        foreach(PhysicalObject item in physicalObjects)
        {
            if (item != this)
            {
                Vector3 direction = (transform.position - item.transform.position).normalized;
                float force = (Const.G * mass * item.mass) / Mathf.Pow
                (Mathf.Clamp(Vector3.Distance(transform.position, item.transform.position), 1f, 200f), 3);
                rb.AddForce(-direction * force / mass, ForceMode.Acceleration);
            }
        }
    }

    protected float GetMass()
    {
        return mass;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * 20, ForceMode.Impulse);
        }
    }
}
