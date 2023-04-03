/*
 Instantiate the text UI elements for the Weapon inventory with custom name and weapon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] GameObject weaponUI;
    [SerializeField] string weaponName;
    [SerializeField] GameObject Text;
    // Start is called before the first frame update

    public void InstantiateWeaponsUI()
    {
        GameObject go = Instantiate(Text, weaponUI.transform, weaponUI.transform);
        go.GetComponent<TextMeshProUGUI>().text = weaponName;
        go.GetComponent<ItemDrag>().weapon = gameObject;
    }

}
