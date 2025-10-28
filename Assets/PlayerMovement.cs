using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;


    //movement settings
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpheight = 1.5f;

    //gravity setup 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; 
    public bool isGrounded = false;

    // initial velocity 
    Vector3 velocity;

    //run before first frame of game
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // run each time per frame

    void Update()
    {
        //checking grounded or not 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //reseting downward force if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        float x = Input.GetAxis("Horizontal"); // A - D left and right 
        float z = Input.GetAxis("Vertical");// W - S , forward , backward ,

        //convert axis input into movement 
        // transform.right -> local X axis
        //transform.forward -> local Z axis
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        //to apply gravity contineously
        velocity.y += gravity * Time.deltaTime;
        //to get vertical movement
        controller.Move(velocity * Time.deltaTime);



    }
    
   
}
