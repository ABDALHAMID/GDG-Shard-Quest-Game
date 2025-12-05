using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 5f;
    public int damage = 10;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime); // Destroy after some time
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if hit object has Health component
        HealthSystem targetHealth = collision.gameObject.GetComponent<HealthSystem>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Optional: spawn impact VFX here
        // Example: Instantiate(hitVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
