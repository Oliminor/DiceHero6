/*
 This script containts the main functions for the Ludo enemy character.
 The movement and the attack functions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudoManager : MonoBehaviour
{
    enum STATUS { MOVE, ATTACK };
    STATUS status;

    [SerializeField] GameObject stompAir;
    [SerializeField] GameObject stompSmallProjectile;
    [SerializeField] GameObject ultimateParticleCharge;
    [SerializeField] GameObject ultimateChargeProjectile;

    public int health;
    [SerializeField] float movementSpeed;
    private float tempSpeed;
    [SerializeField] float attackRange;
    float stompTimer;

    Transform player;

    private Vector3 start;
    private Vector3 des;

    bool switchBool;
    bool stompBool;
    bool ultimateAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case STATUS.MOVE:
                LudoMovement();
                break;
            case STATUS.ATTACK:
                LudoAttack();
                break;
        }
    }

    private void LudoAttack()
    {
        if (stompTimer <= 0 && IsDown())
        {
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));
            // If the distance between the character ant the enemy greater, the Ludo starts walking towards the player
            if (distance > attackRange)
            {
                status = STATUS.MOVE;
                // Switches on the floating movement
                GetComponentsInChildren<Floating>()[0].enabled = true;
                return;
            }
        }

        VectorLerp();
        SwitchDirection();

        stompTimer -= Time.deltaTime;
    }

    private void LudoMovement ()
    {
        tempSpeed = movementSpeed;

        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));

        // if the distance less than the attack range, it switch on the attack mode
        if (distance < attackRange && status == STATUS.MOVE && GetComponentsInChildren<Floating>()[0].isDown() && !stompBool)
        {
            status = STATUS.ATTACK;
            newPlayerPos();
            GetComponentsInChildren<Floating>()[0].enabled = false;
            return;
        }

        stompEffect();
        // Moves towards the player 
        StaticBoard.FollowTarget(player.position, tempSpeed, this.transform);

    }

    private void stompEffect ()
    {
        // if this gameobject isUp (because the up and down motion, it has to be checked if the gameobject is Up or Down)
        // switch on the particles
        if (GetComponentsInChildren<Floating>()[0].isUp())
        {
            stompBool = true;
            stompAir.GetComponent<ParticleSystem>().Play();
        }
        // if isDown, switch off the particle
        else if (GetComponentsInChildren<Floating>()[0].isDown())
        {
            stompAir.GetComponent<ParticleSystem>().Stop();
            tempSpeed = 0;
            if (stompBool)
            {
                // When the object is at the bottom, shakes the camera and instantiate particle effect.
                FindObjectOfType<CameraShake>().cameraStart(3);
                GameObject go = Instantiate(stompSmallProjectile, transform.position, Quaternion.identity);
                Destroy(go, 1);
                stompBool = false;
            }
        }
    }

    //Checks if the transform is at the top of the upward motion
    private bool IsDown ()
    {
        if (Mathf.Abs(transform.position.y - des.y) < 0.3) return true;
        else return false;
    }

    //Checks if the transform is at the bottom of the downward motion
    private bool IsUp()
    {
        if (Mathf.Abs(transform.position.y - start.y) < 0.5) return true;
        else return false;
    }

    private void SwitchDirection()
    {
        // Ultimate attack
        if (IsDown())
        {
            if (stompTimer <= 0)
            {
                switchBool = true;

                if (stompTimer <= 0 && ultimateAttack)
                {
                    ultimateParticleCharge.GetComponent<ParticleSystem>().Stop();
                    GameObject.Find("Stomp").GetComponent<AudioSource>().Play();
                    ultimateAttack = false;
                    stompTimer = 6;
                    GameObject go = Instantiate(ultimateChargeProjectile, transform.position, Quaternion.identity);
                    Destroy(go, 1);
                }
            }
        }
        // When the character is at the top it waits 2 seconds before stomps
        else if (IsUp())
        {
            ultimateParticleCharge.GetComponent<ParticleSystem>().Play();
            ultimateAttack = true;
            switchBool = false;
            if(stompTimer <= 0) stompTimer = 2;
        }
    }

    private void VectorLerp()
    {
        if (stompTimer <= 0)
        {
            if (switchBool) transform.position = Vector3.Lerp(transform.position, start, 5 * Time.deltaTime);
            else transform.position = Vector3.Lerp(transform.position, des, 25 * Time.deltaTime);
        }
    }

    // stomps the enemy position, so it checks where is the player
    private void newPlayerPos()
    {
        start = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z);
        des = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
