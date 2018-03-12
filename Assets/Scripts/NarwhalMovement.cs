using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarwhalMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float maxMovementSpeed = 50f;
    [SerializeField]
    float rotateSpeed = 5f;
    [SerializeField]
    float jumpForce = 15f;
    [SerializeField]
    float gravity = 30f;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    float brakeMultiplier = .99f;
    [SerializeField]
    float moveUpAndDownDelay = 2f;
    [SerializeField]
    int debugPlayerNumberOverride = 0;
    [SerializeField]
    AudioSource hitGroundAudio;

    private Rigidbody narwhalRigidbody;

    private Text playerNumberText;
    private Player playerNumber_UseProperty;
    private CharacterController controller;

    float horizontalInput;
    float verticalInput;
    string fireButton;
    string horizontalAxis;
    string verticalAxis;

    //FROM DAVID ANTOGNOLI'S JOIN SCREEN LAMBDA EXAMPLE
    public Player ControlPlayerNumber
    {
        get { return playerNumber_UseProperty; }
        set
        {
            playerNumber_UseProperty = value;
            UpdatePlayerIndexLabelText();
        }
    }

    private void UpdatePlayerIndexLabelText()
    {
        playerNumberText.text = ControlPlayerNumber.PlayerNumber.ToString();
    }

    private void Awake()
    {
        narwhalRigidbody = GetComponent<Rigidbody>();
        playerNumberText = GetComponentInChildren<Text>();
        hitGroundAudio = GetComponent<AudioSource>();

#if UNITY_EDITOR
        if (debugPlayerNumberOverride > 0)
        {
            ControlPlayerNumber = new Player(debugPlayerNumberOverride);
        }
#endif
    }

    // Use this for initialization
    void Start()
    {       
        controller = gameObject.GetComponent<CharacterController>();
        //MAKE SURE you change input tags to match certain player numbers
        //horizontalAxis = "Horizontal";
        //verticalAxis = "Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        //InputHandler();
        PlayerMoveAndJump();
    }

    //void FixedUpdate()
    //{
    //    Rotate();
    //    Move();
    //    AdjustNarwhalVelocity();
    //    Jump();
    //}

    //This uses CharacterController - must change
    private void PlayerMoveAndJump()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= moveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                hitGroundAudio.Play();
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //FROM TANKS TUTORIAL BY UNITY
    void Rotate()
    {
        float turn = horizontalInput * rotateSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        narwhalRigidbody.angularVelocity = Vector3.zero;
        narwhalRigidbody.MoveRotation(narwhalRigidbody.rotation * turnRotation);
    }

    //void InputHandler()
    //{
    //    horizontalInput = Input.GetAxis("Horizontal");
    //    verticalInput = Input.GetAxis("Vertical");
    //}

    //void Move()
    //{
    //    Vector3 narwhalMover = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;
    //    StartCoroutine("BouncingNarwhal");
    //    narwhalRigidbody.AddForce(narwhalMover);
    //}

    //void Jump()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        moveDirection.y = jumpForce;
    //        hitGroundAudio.Play();
    //    }
    //    moveDirection.y -= gravity * Time.deltaTime;
    //}

    //void AdjustNarwhalVelocity()
    //{
    //    if (Mathf.Sqrt(narwhalRigidbody.velocity.sqrMagnitude) >= maxMovementSpeed)
    //        narwhalRigidbody.velocity *= brakeMultiplier;
    //}

    //IEnumerator BouncingNarwhal()
    //{
    //    narwhalRigidbody.velocity = Vector3.zero;
    //    narwhalRigidbody.angularVelocity = Vector3.left * rotateSpeed;
    //    yield return new WaitForSeconds(moveUpAndDownDelay);
    //    narwhalRigidbody.angularVelocity = Vector3.zero;
    //}
}


