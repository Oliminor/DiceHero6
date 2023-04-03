/*
 Called GameManager but this scripts just manage the Camera Switch and checks if the enemy is on the screen or not, should called CameraManager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject lockCamera;

    public Transform respawnPoint;
    public Transform tutorialRespawnPoint;
    public Transform mainRespawnPoint;

    public List<Texture2D> cardTexture;
    // Start is called before the first frame update
    void Start()
    {
        SetCheckPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }


    public void SetCheckPoint()
    {
        if (StaticBoard.tutorialBool) respawnPoint = tutorialRespawnPoint;
        else respawnPoint = mainRespawnPoint;
    }

    private GameObject EnemyVisible ()
    {
        GameObject lockTarget = null;
        GameObject[] enemiesOnScreen;

        enemiesOnScreen = GameObject.FindGameObjectsWithTag("enemy");

        Vector3 lookAtPosition = Camera.main.WorldToScreenPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        float currentDistance = Mathf.Infinity;

        foreach (GameObject enemies in enemiesOnScreen)
        {
            if (enemies.GetComponent<Renderer>().isVisible)
            {
                Vector3 enemyPosition = Camera.main.WorldToScreenPoint(enemies.transform.position);
                float distance = Vector3.Distance(lookAtPosition, enemyPosition);

                if (distance < currentDistance)
                {
                    lockTarget = enemies;
                    currentDistance = distance;
                }
            }
        }
        return lockTarget;
    }

    public void SwitchCamera()
    {
        GameObject lockTarget = EnemyVisible();

        if (lockTarget)
        {
            if (playerCamera != null || lockCamera != null)
            {
                if (playerCamera.activeSelf)
                {
                    EnemyVisible();
                    LockOnCamera.lockBool = false;
                    playerCamera.SetActive(false);
                    lockCamera.SetActive(true);
                    lockCamera.GetComponent<CinemachineFreeLook>().LookAt = lockTarget.transform;


                }
                else if (lockCamera.activeSelf)
                {
                    playerCamera.SetActive(true);
                    lockCamera.SetActive(false);
                    playerCamera.GetComponent<CinemachineFreeLook>().m_XAxis.Value = lockCamera.GetComponent<CinemachineFreeLook>().m_Heading.m_Bias;
                    playerCamera.GetComponent<CinemachineFreeLook>().m_Heading.m_Bias = 0;
                }
            }
        }
    }
}
