/*
 This script just manage the dash orb UI, if the player uses dash, one orb disappear or appear again if the cooldown is done
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashOrb : MonoBehaviour
{
    [SerializeField] int OrbCheck;
    private Color color;

    private void Start()
    {
        color = gameObject.GetComponent<Image>().color;
    }

    void Update()
    {
        if (PlayerManager.dashNumber < OrbCheck)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            gameObject.GetComponent<Image>().color = color;
        }
    }
}