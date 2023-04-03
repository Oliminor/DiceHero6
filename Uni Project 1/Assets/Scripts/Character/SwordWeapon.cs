/*
 Sword weapon script, when the player click with the left mouse button, the weapons performs an attack, the next attack could only happens
 after the first one animation ends. If this object is disabled, every child object disabled aswell for prevent some visual bugs.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject weapon;
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        weapon.SetActive(true);
    }

    private void OnDisable()
    {
        StaticBoard.meleeAttack = true;
        Transform[] childs = GetComponentsInChildren<Transform>(true);
        foreach (var child in childs) child.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.jumpBool && !PlayerManager.dashBool) Attack();
        StaticBoard.meleeAttack = isAttacking();
    }

    void Attack()
    {
        if (StaticBoard.isCharacterActive)
        {
            if (Input.GetMouseButton(0) && isAttacking())
            {
                GameObject.Find("SwordSlash").GetComponent<AudioSource>().Play();
                transform.rotation = Quaternion.Euler(0f, PlayerManager.direction, 0f);
                GameObject.FindWithTag("Player").transform.rotation = Quaternion.Euler(0f, PlayerManager.direction, 0f);
                anim.SetTrigger("Attack01");
            }
        }
    }

    public bool isAttacking()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("attackTrigger")) return true;
        else return false;
    }
}
