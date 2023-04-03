/*
 If the player is damaged, this script activates
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    bool playerDamaged = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!playerDamaged)
        {
            if (other.tag == "Player")
            {
                FindObjectOfType<CubeRotate>().isDamaged();
                playerDamaged = true;
            }
        }
    }
}
