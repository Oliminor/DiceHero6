/*
 This script adjust the LockOnCamera behind the player, so both the player and the enemy target is on the middle (Horizontally at least)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOnCamera : MonoBehaviour
{
    private CinemachineFreeLook cam;
    public static bool lockBool = false;
    public static float distance;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam.LookAt) FindObjectOfType<GameManager>().SwitchCamera();
        else
        {
            Vector2 player = new Vector2(cam.Follow.position.x, cam.Follow.position.z);
            Vector2 target = new Vector2(cam.LookAt.position.x, cam.LookAt.position.z);
            Vector2 direction = target - player;

            distance = Vector3.Distance(player, target);

            float angle = Mathf.Atan2(direction.x, direction.y) * 180.0f / 3.14f;
            float tempBias = cam.m_Heading.m_Bias;
            cam.m_Heading.m_Bias = angle;

            //If the distance less than 10, the LockOnCamera switch back to the normal camera
            if (distance < 10 && !lockBool)
            {
                FindObjectOfType<GameManager>().SwitchCamera();
                lockBool = true;
            }
        }
    }
}
