/*
 Change tag after its enabled first time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTag : MonoBehaviour
{
    [SerializeField] string newTag;
    void Start()
    {
        gameObject.tag = newTag;
    }

    // Update is called once per frame
}
