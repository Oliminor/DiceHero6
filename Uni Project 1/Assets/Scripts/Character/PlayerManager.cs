/*
 Every character movement functions are here (Movement, Jump, Dash).
 
 The movement is based on the camera rotation, the forward is always same as the camera forward.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject dashParticle;

    public LayerMask whatIsGround;
    public static int health = 6;
    public static int dashNumber = 3;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpUpModifier;
    [SerializeField] private float jumpDownModifier;
    [SerializeField] private float dashTimer = 1;

    float smoothTurn;
    public static float direction;

    private InputHandler input;
    private Rigidbody rb;
    public static bool jumpBool;
    public static bool dashBool = false;
    private bool zeroBool = true;
    private float tempDashTimer;

    void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        tempDashTimer = dashTimer;
    }
    private void Update()
    {
        Jump();
        Dash();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (StaticBoard.isCharacterActive)
        {
            if (transform.position.y < 30)
            {
                transform.position = FindObjectOfType<GameManager>().respawnPoint.position;
            }

            // Input vector from the Input Handler
            Vector3 targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);

            float targetAngle = transform.eulerAngles.y;

            // Only rotate the character if its moving
            if (input.InputVector.x != 0 || input.InputVector.y != 0)
            {
                targetAngle = Mathf.Atan2(targetVector.x, targetVector.z) * Mathf.Rad2Deg + Camera.main.gameObject.transform.eulerAngles.y;
                zeroBool = false;
            }
            else zeroBool = true;

            direction = targetAngle;

            // Only move the character if not jumping or attacking
            if (StaticBoard.meleeAttack && !jumpBool)
            {
                // Smooth turning
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurn, turnSmoothTime);
                if (input.InputVector.x != 0 || input.InputVector.y != 0)
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Direction based on the camera rotation
                Vector3 newDirection = Quaternion.Euler(0, Camera.main.gameObject.transform.eulerAngles.y, 0) * targetVector;
                rb.velocity = newDirection * moveSpeed;
            }
            else
            {
                if (!jumpBool) rb.velocity = new Vector3(0, 0, 0);
                else
                {
                    // Physics calculation for jump
                    if (rb.velocity.y < 0) rb.velocity += Vector3.down * jumpDownModifier * Time.deltaTime;
                    else if (rb.velocity.y > 0) rb.velocity += Vector3.down * jumpUpModifier * Time.deltaTime;
                }
            }

            //The character move forward slowly while attack
            if (!StaticBoard.meleeAttack)
            {
                rb.velocity = transform.forward * (moveSpeed / 5);
            }

            if (health <= 0)
            {
                StaticBoard.isCharacterActive = false;
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void Jump()
    {
        if (StaticBoard.isCharacterActive)
        {
            // Raycast checks if the character on the ground
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, 0.5f, whatIsGround))
            {
                jumpBool = false;
                if (Input.GetKeyDown(KeyCode.Space) && StaticBoard.meleeAttack && !dashBool)
                {
                    // After the charater jumps, it ignores the smooth turning and snap into the diraction it should face based
                    // on the Input Handler
                    transform.rotation = Quaternion.Euler(0f, direction, 0f);
                    rb.AddForce(Vector3.up * jumpForce);
                }
            }
            else jumpBool = true;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !zeroBool && !jumpBool && dashNumber > 0) StartCoroutine(DashC());

        tempDashTimer -= Time.deltaTime;

        // Dash cooldown
        if (dashNumber < 3)
        {
            if (tempDashTimer <= 0)
            {
                dashNumber++;
                tempDashTimer = dashTimer;
            }
        }
    }

    // Dash courutine 
    IEnumerator DashC()
    {
        tempDashTimer = dashTimer;
        dashNumber--;
        float tempSpeed = moveSpeed;
        moveSpeed *= 3;
        dashBool = true;
        transform.rotation = Quaternion.Euler(0f, direction, 0f);
        GameObject go = Instantiate(dashParticle, transform.position, Quaternion.Euler(0,transform.eulerAngles.y,0));
        Destroy(go, 1);

        //yield on a new YieldInstruction that waits for 0.1 seconds.
        yield return new WaitForSeconds(0.1f);
        moveSpeed = tempSpeed;
        dashBool = false;
    }
}
