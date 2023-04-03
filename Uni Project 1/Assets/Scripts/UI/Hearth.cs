/*
  This script just manage the health heart UI, if the player loses health, the hearth disappear
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearth : MonoBehaviour
{
    [SerializeField] int healthCheck;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.health < healthCheck)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
