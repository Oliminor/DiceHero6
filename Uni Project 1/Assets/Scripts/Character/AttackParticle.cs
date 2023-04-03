/*
 This scripts emits particles when the swords hits the enemy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" && !FindObjectOfType<SwordWeapon>().isAttacking())
        {
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            GameObject go = Instantiate(particle, closestPoint, Quaternion.identity);
            Destroy(go, 1);
            other.GetComponent<TakeDamage>().Damaged(1);
        }
    }
}
