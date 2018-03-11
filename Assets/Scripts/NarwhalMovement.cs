using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarwhalMovement : MonoBehaviour
{

    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float jumpForce = 15f;
    [SerializeField]
    float gravity = 30f;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;

    private Rigidbody narwhalRigidbody;
    private Text playerNumberText;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        narwhalRigidbody = GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
}


