/*
 When the Ludo is ready to fight, this script enables every Ludo related scripts
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudoSpawn : MonoBehaviour
{
    [SerializeField] int spawnIndex;
    bool spawnBool = false;

    void Update()
    {
        if (spawnIndex == StaticBoard.LudoIndex && spawnIndex != 0 && !spawnBool)
        {
            gameObject.GetComponent<LudoManager>().enabled = true;
            gameObject.GetComponentInChildren<Floating>().enabled = true;
            gameObject.GetComponentInChildren<TakeDamage>().enabled = true;
            gameObject.GetComponentInChildren<ChangeTag>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            spawnBool = true;
        }
    }
}
