/*
 Projectile script using Addforce and the direction is the Camera forward direction
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject explodeParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * 200, ForceMode.Impulse);
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<TakeDamage>().Damaged(2);
        }

        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject go = Instantiate(explodeParticle, transform.position, Quaternion.identity);
            Destroy(go, 1);
        }
    }
}
