/*
Dragging items for in the weapon inventory and changeing slots (indexes in this case)
The UI element are interactable and with the Handlers the mouse can drag them
Uising getsiblingIndex the script able to change the item order and at the same time
the weapon what the player use
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject weapon;
    GameObject dragItem;
    Vector3 startPosition;
    int startIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startIndex = transform.GetSiblingIndex();
        dragItem = gameObject;
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragItem = null;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        transform.position = startPosition;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("weaponSlot"))
            {
                transform.position = result.gameObject.transform.position;
                transform.SetSiblingIndex(result.gameObject.transform.GetSiblingIndex());
                weapon.transform.SetSiblingIndex(result.gameObject.transform.GetSiblingIndex());


                result.gameObject.transform.position = startPosition;
                result.gameObject.transform.SetSiblingIndex(startIndex);
                result.gameObject.GetComponent<ItemDrag>().weapon.transform.SetSiblingIndex(startIndex);
            }            
        }

        GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
        FindObjectOfType<WeaponHandler>().RefreshList();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}