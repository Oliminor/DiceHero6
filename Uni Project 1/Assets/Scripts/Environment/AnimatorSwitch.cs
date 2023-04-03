/*
 Switch off the the Arena animation after its done with the animation (for prevent any bugs)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSwitch : MonoBehaviour
{
    public bool animatorSwitchOff = false;

    void Update()
    {
        if (animatorSwitchOff)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<AnimatorSwitch>().enabled = false;
            StaticBoard.LudoIndex = 1;
        }
    }
}
