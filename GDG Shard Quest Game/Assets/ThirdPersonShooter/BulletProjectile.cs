using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 500f;
        bulletRigidbody.linearVelocity = transform.forward * speed;
        StartCoroutine(DestroyAfterTime(3f));
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<BulletTarget>() != null)
        //{
        //    Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        //}
        //else
        //{
        //    Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        //}
        Destroy(gameObject);
    }
    
    private IEnumerator DestroyAfterTime(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}