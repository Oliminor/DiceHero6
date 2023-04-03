/*
 Enemy damage manager, when the enemy character suffer damage, this scripts activates
 also when the enemy character health less than 0, emits particles.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject deathParticle;
    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void Damaged (int damageID)
    {
        if (gameObject.GetComponent<TakeDamage>().enabled == true)
        {
            StartCoroutine(StaticBoard.TurnRedDamageIndicator(gameObject));
            GetComponentInParent<LudoManager>().health -= StaticBoard.PlayerDealDamage(damageID);
            GameObject.Find("SwordHit").GetComponent<AudioSource>().Play();

            if (GetComponentInParent<LudoManager>().health <= 0)
            {
                GameObject go = Instantiate(deathParticle, transform.position, Quaternion.identity);
                Destroy(go, 5);
                Destroy(gameObject.transform.parent.gameObject);

                StaticBoard.LudoIndex++;
            }
        }
    }
}
