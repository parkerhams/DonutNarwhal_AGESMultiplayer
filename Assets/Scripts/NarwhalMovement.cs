using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarwhalMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 10f;
    //[SerializeField]
    //float rotateSpeed = 5f;
    [SerializeField]
    float jumpForce = 15f;
    [SerializeField]
    float gravity = 30f;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;

    private Rigidbody narwhalRigidbody;
    int playerNumber_UseProperty;
    private Text playerNumberText;
    private CharacterController controller;
    //float horizontalInput;
    //float verticalInput;
    //string horizontalAxis;
    //string verticalAxis;

    //public int PlayerNumber
    //{
    //    get { return playerNumber_UseProperty; }
    //    set { playerNumber_UseProperty = value; }
    //}

    // Use this for initialization
    void Start()
    {
        narwhalRigidbody = GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        //horizontalAxis = "P" + PlayerNumber + "-Horizontal";
        //verticalAxis = "P" + PlayerNumber + "-Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        //HandleInput();
        PlayerMoveAndJump();
    }

    private void PlayerMoveAndJump()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //void Rotate()
    //{
    //    float turn = horizontalInput * rotateSpeed * Time.fixedDeltaTime;
    //    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
    //    narwhalRigidbody.angularVelocity = Vector3.zero;
    //    narwhalRigidbody.MoveRotation(narwhalRigidbody.rotation * turnRotation);
    //}

    //void HandleInput()
    //{
    //    horizontalInput = Input.GetAxis("Horizontal");
    //    verticalInput = Input.GetAxis("Vertical");
    //    //shouldShoot = Input.GetButtonDown(fireButton);
    //}
}


