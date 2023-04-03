/*
 Triggers text when the player step on the trigger collider!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    [SerializeField] GameObject tutorialText;
    bool textActive = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !textActive)
        {
            if(tutorialText.activeSelf == false)tutorialText.SetActive(true);
            textActive = true;
        }
    }
}
