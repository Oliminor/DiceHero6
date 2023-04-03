/*
 Switching weapon after lose health point
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Transform[] Weapons;
    int tempHealth;
    int currentHealth;
    bool startBool = true;
    // Start is called before the first frame update
    void Start()
    {
        RefreshList();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerManager.health;

        if (tempHealth != currentHealth)
        {
            RefreshList();
        }

        tempHealth = PlayerManager.health;
    }

    public void RefreshList()
    {
        DisableAllWeapon();
        SwitchNewWeapon();
    }

    public void DisableAllWeapon()
    {
        Weapons = transform.GetComponentsInChildren<Transform>(true);

        foreach (Transform weapon in Weapons)
        {
            if (weapon.CompareTag("PlayerWeapon"))
            {
                if (startBool)
                {
                    weapon.GetComponent<WeaponInventory>().InstantiateWeaponsUI();
                }
                weapon.gameObject.SetActive(false);
            }
        }
        startBool = false;
    }

    public void SwitchNewWeapon()
    {
        int index = PlayerManager.health - 1;
        if (index < 0) index = 0;
        if (index < Weapons.Length) transform.GetChild(index).gameObject.SetActive(true);
    }
}
