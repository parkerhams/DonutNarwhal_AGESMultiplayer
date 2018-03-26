using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarwhalMoveAndTurn : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float rotateSpeed = 5f;
    [SerializeField]
    float jumpForce = 15f;
    [SerializeField]
    float gravity = 30f;
    // Projectile speed
    [SerializeField]
    float fireSpeed = 5f;
    [SerializeField]
    float fireCooldown = 2f;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform shootPoint;
    //[SerializeField]
    //int debugPlayerNumberOverride = 0;
    [SerializeField]
    AudioSource hitGroundAudio;

    bool canShoot = true;
    bool shouldShoot;

    private Rigidbody narwhalRigidbody;
    private float Speed = 10f;
    private string turnAxis;
    private string movementAxis;

    private Text playerNumberText;
    //private Player playerNumber_UseProperty;
    
    private int scoreCount;

    GameObject activeProjectile;

    int playerNumber_UseProperty;
    float horizontalInput;
    float verticalInput;
    string fireButton;
    string horizontalAxis;
    string verticalAxis;

    [HideInInspector]
    public bool isAlive = true;

    //public Text countText;
    //public Text winText;

    //public int playerNumber = 1;

    //FROM DAVID ANTOGNOLI'S JOIN SCREEN LAMBDA EXAMPLE
    // Which player controls the character?
    // We will use the Player.PlayerNumber to
    // decide which GamePad to look at.
    public int PlayerNumber
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
        playerNumberText.text = PlayerNumber.ToString();
    }

    #region private properties
    private float HorizontalInput
    {
        get { return Input.GetAxis(HorizontalInputName); }
    }

    private float VerticalInput
    {
        get { return Input.GetAxis(VerticalInputName); }
    }

    // You must configure the Unity Input Manager
    // to have an axis for each control for each supported player.
    // Begin numbering at 1, as Unity numbers "joysticks" beginning at 1.
    // "Joystick" means gamepad in real life...
    private string HorizontalInputName
    {
        get
        {
            return "Horizontal" + PlayerNumber;
        }
    }

    private string VerticalInputName
    {
        get
        {
            return "Vertical" + PlayerNumber;
        }
    }

    private string FireInputName
    {
        get
        {
            return "Fire" + PlayerNumber;
        }
    }
    #endregion

    private void Awake()
    {
        narwhalRigidbody = GetComponent<Rigidbody>();
        playerNumberText = GetComponentInChildren<Text>();
        hitGroundAudio = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        //MAKE SURE you change input tags to match certain player numbers
        //horizontalAxis = "Horizontal" + debugPlayerNumberOverride;
        //verticalAxis = "Vertical" + debugPlayerNumberOverride;

        //horizontalAxis = HorizontalInputName;
        //verticalAxis = VerticalInputName;
        //fireButton = FireInputName;

        scoreCount = 0;
        //SetCountText();
        //winText.text = "";
    }

    private void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moveSpeed = Input.GetAxis(movementAxis);
        //rotateSpeed = Input.GetAxis(turnAxis);
        Move();
        Rotate();

        if(shouldShoot && canShoot)
        {
            Fire();
        }
    }

    void HandleInput()
    {
        horizontalInput = Input.GetAxis(HorizontalInputName);
        verticalInput = Input.GetAxis(VerticalInputName);
        shouldShoot = Input.GetButtonDown(FireInputName);

    }

    private void Move()
    {
        moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        narwhalRigidbody.velocity = moveDirection * moveSpeed;
        //narwhalRigidbody.MovePosition(narwhalRigidbody.position + moveDirection);
    }

    //FROM TANKS TUTORIAL BY UNITY
    void Rotate()
    {
        float turn = horizontalInput * rotateSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        narwhalRigidbody.MoveRotation(narwhalRigidbody.rotation * turnRotation);
    }

    void Fire()
    {
        activeProjectile = Instantiate(projectile, shootPoint.position, shootPoint.transform.rotation);
        activeProjectile.transform.Rotate(Vector3.right, -90f);
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), activeProjectile.GetComponent<Collider>());
        activeProjectile.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * fireSpeed;

        StartCoroutine(ShootCooldown());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Ups"))
        {
            other.gameObject.SetActive(false);
            scoreCount = scoreCount + 1;
            //SetCountText();
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCooldown);
        canShoot = true;
    }
    //void SetCountText()
    //{
    //    countText.text = "Donuts: " + scoreCount.ToString();
    //    winText.text = "";

    //    if (scoreCount >= 1)
    //    {
    //        countText.text = "";
    //        winText.text = "" + PlayerNumber + " Wins!";
    //    }
    //}
}
