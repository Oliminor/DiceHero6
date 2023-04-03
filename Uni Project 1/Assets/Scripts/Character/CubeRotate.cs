/*
 Rotates the cube when health point is changed, the top texture is always the same number as the player health number
 And also a damage indicator turns red and emits particles when the player health decreased.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    public GameObject damageParticle;

    private float invulnerability = 2f;
    private Vector3 des;
    private Color baseColor;

    private float tempHealth;
    // Start is called before the first frame updatem
    void Awake()
    {
        tempHealth = PlayerManager.health;
        baseColor = gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor");
    }

    // Update is called once per frame
    void Update()
    {
        RotatedSide(PlayerManager.health);
        RotateLerp();
        invulnerability -= Time.deltaTime;
    }

    void RotateLerp()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(des), rotateSpeed * Time.deltaTime);
    }

    void RotatedSide(int health)
    {
        switch (health)
        {
            case 1:
                des = new Vector3(0, 0, 0);
                break;

            case 2:
                des = new Vector3(270, 0, 0);
                break;

            case 3:
                des = new Vector3(180, 0, 270);
                break;

            case 4:
                des = new Vector3(180, 0, 90);
                break;

            case 5:
                des = new Vector3(90, 0, 0);
                break;

            case 6:
                des = new Vector3(180, 0, 0);
                break;
        }
    }

    public void isDamaged ()
    {
        if (invulnerability <= 0)
        {
            PlayerManager.health--;
            GameObject damageP = Instantiate(damageParticle, transform.position, Quaternion.identity, transform.parent);
            Destroy(damageP, 1f);
            StartCoroutine(TurnRedDamageIndicator());
            invulnerability = 2f;
        }
    }

    private IEnumerator TurnRedDamageIndicator()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", baseColor);
    }
}
