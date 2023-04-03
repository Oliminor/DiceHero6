/*
 Up and down motion using Lerp for the sooth tranlaste
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float upperLimit;
    public float lowerLimit;

    private Vector3 start;
    private Vector3 des;
    bool switchBool;

    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(transform.localPosition.x, transform.localPosition.y + upperLimit, transform.localPosition.z);
        des = new Vector3(transform.localPosition.x, transform.localPosition.y - lowerLimit, transform.localPosition.z);
    }

    private void OnDisable()
    {
        transform.localPosition = des;
    }

    // Update is called once per frame
    void Update()
    {
        FloatingDirectionToggle();
        Lerping();
    }

    private void FloatingDirectionToggle ()
    {
        if (Mathf.Abs(transform.localPosition.y - des.y) < 0.01) switchBool = true;
        else if (Mathf.Abs(transform.localPosition.y - start.y) < 0.01) switchBool = false;
    }

    private void Lerping()
    {
        if (switchBool) transform.localPosition = Vector3.Lerp(transform.localPosition, start, floatSpeed * Time.deltaTime);
        else transform.localPosition = Vector3.Lerp(transform.localPosition, des, floatSpeed * Time.deltaTime);
    }

    public bool isUp()
    {
        if (Mathf.Abs(transform.localPosition.y - start.y) < 0.2) return true;
        else return false;
    }

    public bool isDown()
    {
        if (Mathf.Abs(transform.localPosition.y - des.y) < 0.5) return true;
        else return false;
    }
}
