/*
 Script to trigger the tutorial texts
 Deactivate them after the player finished the tutorial
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject movementTutorial;
    [SerializeField] GameObject jumpTutorial;
    [SerializeField] GameObject dashTutorial;
    [SerializeField] GameObject attackTutorial;
    [SerializeField] GameObject finishTutorial;
    [SerializeField] GameObject finishTrigger;

    // Update is called once per frame
    void Update()
    {
        MovementTutorial();
        JumpTutorial();
        DashTutorial();
        AttackTutorial();
        FinishTutorial();
    }

    void FinishTutorial()
    {
        if (finishTrigger.activeSelf == true)
        {
            gameObject.SetActive(false);
            finishTrigger.SetActive(false);
            Transform player = GameObject.FindWithTag("Player").transform;
            StaticBoard.tutorialBool = false;
            FindObjectOfType<GameManager>().SetCheckPoint();
            player.position = FindObjectOfType<GameManager>().respawnPoint.position;
        }
    }

    void DashTutorial()
    {
        if (dashTutorial.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(SetDeactivate(dashTutorial));
            }

            if (attackTutorial.activeSelf == true)
            {
                dashTutorial.SetActive(false);
            }
        }
    }

    void JumpTutorial()
    {
        if (jumpTutorial.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SetDeactivate(jumpTutorial));
            }

            if (dashTutorial.activeSelf == true)
            {
                jumpTutorial.SetActive(false);
            }
        }
    }

    void AttackTutorial()
    {
        if (attackTutorial.activeSelf == true)
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(SetDeactivate(attackTutorial));
                StartCoroutine(finishActivate());
            }
        }
    }

    void MovementTutorial()
    {
        if (movementTutorial.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(SetDeactivate(movementTutorial));
            }

            if (jumpTutorial.activeSelf == true)
            {
                movementTutorial.SetActive(false);
            }
        }
    }

    IEnumerator SetDeactivate(GameObject tutorialText)
    {
        yield return new WaitForSeconds(3.0f);
        tutorialText.SetActive(false);
    }

    IEnumerator finishActivate()
    {
        yield return new WaitForSeconds(3.0f);
        finishTutorial.SetActive(true);
    }
}
