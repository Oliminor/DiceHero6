/*
 Summong the spectator cards on the terrace gameobject 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrace : MonoBehaviour
{
    public int cardNumber;
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        Tiling();
        SpawnCards();
    }

    // Update is called once per frame
    private void Tiling()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetVector("_BaseMap_ST", new Vector2(gameObject.transform.parent.localScale.x  / 2, 1));
    }

    private void SpawnCards()
    {
        float stepSize = GetComponent<BoxCollider>().size.x / cardNumber;
        float boundSize = GetComponent<BoxCollider>().size.x;

        for (int i = 0; i < cardNumber; i++)
        {
            Vector3 position = new Vector3(0,0,0);
            GameObject go = Instantiate(card, position, transform.rotation, transform);
            go.transform.localPosition = position;
            Vector3 localPosition = new Vector3(go.transform.localPosition.x + (boundSize / 2) - (stepSize * i) - stepSize / 2, 1.5f, 0.5f);
            go.transform.localPosition = localPosition;
            go.transform.localScale = new Vector3(3 / transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }
}
