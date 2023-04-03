/*
 Simple Input Handler to store the W A S D and Arrow keys vertical and horinzontal input for movmenet.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        InputVector = new Vector2(h, v);
        if (InputVector.magnitude > 1) InputVector = InputVector.normalized;
    }
}
