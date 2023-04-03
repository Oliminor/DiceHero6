/*
 This script moves the transform on a path and when it reaches the end, just change the position back to the beginning and start over
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowd : MonoBehaviour
{
    public List<Vector3> path;
    public int pathIndex;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", Random.ColorHSV());
        gameObject.GetComponentInChildren<Animator>().Play(0, -1, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromNextNode = Vector3.Distance(path[pathIndex], transform.position);

        if (distanceFromNextNode < 5f)
        {
            pathIndex++;

            if (pathIndex > path.Count - 1)
            {
                pathIndex = 0;
                transform.position = path[0];
                transform.LookAt(path[1]);
            }
        }

        transform.Translate(Vector3.forward * Time.deltaTime * 10);
        Quaternion newRotation = Quaternion.LookRotation(path[pathIndex] - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * 100);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
