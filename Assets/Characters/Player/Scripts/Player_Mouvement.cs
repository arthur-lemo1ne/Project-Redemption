using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mouvement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public float speed = 12f;
    public float RunningSpeed = 18f;
    public float gravity = -9.81f;
    public float JumpHight = 2f;
    public LayerMask groundMask;

    Vector3 move;
    Vector3 velocity;
    bool isGrounded;
    bool isRunning;
    private float x, z, sp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            velocity.x = 0f;
            velocity.z = 0f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        if (isRunning)
        {
            sp = RunningSpeed;
        }
        else
        {
            sp = speed;
        }

        if (isGrounded)
        {

            controller.Move(move * sp * Time.deltaTime);
           
        }
        else
        {

            controller.Move(move * sp / 2 * Time.deltaTime);

        }
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.x = move.x * sp;
            velocity.z = move.z * sp;
            velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
