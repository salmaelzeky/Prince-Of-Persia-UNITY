using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{
    CharacterController controller = null;
    public Animator anim ;


    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.03f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float gravity;
    [SerializeField] bool lockCursor = true;

    public float cameraPitch = 0.0f;
    public float velocityY = 0.0f;
    public bool isSprinting = false;
    public float sprintingMultiplier;

    public float moveSpeed;
    public float jumpSpeed;

    public int maxHealth = 100;
    public int playerHealth;
    public healthBar healthBar;


    public int score = 0;
    public int playerSandsOfTime = 0;

    public Vector3 moveVector = Vector3.zero;




    Vector2 currentDir = Vector2.zero;
    Vector2 currentVel = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVel = Vector2.zero;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(healthBar != null)
        {
            playerHealth = maxHealth;
            print("Player Health:" + playerHealth);
            healthBar.setMaxHealth(100);
        }

    }

    // Update is called once per frame
    void Update()
    {

        updateMouseLook();
        handleMovement();
        updateMovement();

        //Attacking
        if (Input.GetKey("t"))
        {
            if (healthBar != null && playerHealth > 0)
            {
                takeDamage(10);
            }
        }

    }

    void takeDamage(int damage)
    {

            playerHealth -= damage;
            healthBar.setHealth(playerHealth);
            print("Attacked");
            print("Player Health: " + playerHealth);
    }

    void updateMouseLook()
    {

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVel, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseSensitivity * currentMouseDelta.x);
    }


    void updateMovement()
    {

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentVel, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

    }


    void handleMovement()
    {

        anim.SetFloat("vertical", Input.GetAxis("Vertical"));

        //anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, 1);
        }
        //Attacking
        if (Input.GetKey("m"))
        {
            print("Attack");
            anim.SetBool("attack", true);

        }
        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.UpArrow))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
                walkSpeed += 0.1f;
        }
        else
        {
            walkSpeed = 6;
        }
        //Jumping
        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
        {
            moveVector.y = velocityY;
            controller.Move(moveVector * Time.deltaTime);
            moveVector.y += gravity * -10;
            controller.Move(moveVector * Time.deltaTime);
            print("Jump");
        }



    }



    public void handleRolling()
    {

        print("Roll");
    }


}
