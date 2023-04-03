/*
 Simple fireball Instantiate, from different start position
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject fireBallParticle;
    [SerializeField] Transform[] startPositions;
    [SerializeField] float hitrate = 0.2f;
    [SerializeField] LayerMask WhatToIgnore;
    float tempHitrate;
    int positionCounter = 0;

    void Update()
    {
        FireBallAttack();
        tempHitrate -= Time.deltaTime;
    }

    void FireBallAttack()
    {
        if (StaticBoard.isCharacterActive)
        {
            if (Input.GetMouseButton(0) && tempHitrate <= 0)
            {
                GameObject.Find("Fireball").GetComponent<AudioSource>().Play();
                tempHitrate = hitrate;
                if (positionCounter == startPositions.Length) positionCounter = 0;

                GameObject go = Instantiate(fireBallParticle, startPositions[positionCounter].position, Quaternion.LookRotation(Camera.main.transform.forward.normalized));
                Destroy(go, 5);
                positionCounter++;
            }
        }
    }
}
