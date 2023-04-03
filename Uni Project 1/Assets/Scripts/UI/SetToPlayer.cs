/*
 Sets the UI elements to the player position (dash orb in this case, but every element if you apply this script on it)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToPlayer : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(player.transform.position);
        gameObject.GetComponent<RectTransform>().position = position;
    }
}
