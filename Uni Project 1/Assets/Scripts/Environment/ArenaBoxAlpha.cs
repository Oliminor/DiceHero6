/*
 Provides position value for the arena shader
 This shader lits up (alpha change to 0 to 1) if the player is close to the vertex
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBoxAlpha : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.SetVector("_position", player.transform.position);
    }
}
