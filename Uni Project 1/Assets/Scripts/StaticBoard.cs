/*
 Experiment script, storing major values for the game state, such as if the character active(if not, the character inputs are disabled), 
 attacks, if the player character is in combar or not, should be GameManager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticBoard
{
    public static int LudoIndex = 0;
    public static bool meleeAttack = false;
    public static bool isCharacterActive = true;
    public static bool tutorialBool = true;
    public static bool isBattleActive = false;
    public static void FollowTarget(Vector3 target, float movementSpeed, Transform transform)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        Vector3 lookAt = target;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    public static int PlayerDealDamage(int weaponIndex)
    {
        int damage = 0;
        switch (weaponIndex)
        {
            case 1:
                {
                    damage = Random.Range(10, 20);
                }
                break;
            case 2:
                {
                    damage = Random.Range(5, 10);
                }
                break;
        }
        return damage;
    }

    public static IEnumerator TurnRedDamageIndicator(GameObject gameObject)
    {
        Color baseColor = gameObject.GetComponent<Renderer>().material.color;
        if (baseColor != Color.red)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            yield return new WaitForSeconds(0.15f);
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", baseColor);
        }
    }
}