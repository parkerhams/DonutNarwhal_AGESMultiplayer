using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 50.0f;

    private Quaternion rotate = Quaternion.identity;

    private int rotateY;

    private Animator anim;
    private Rigidbody playerRigidBody;
    //private float tempSpeed;

    //private float moveX;
    //private float moveZ;

    //private SpriteRenderer spriteRenderer;
    


    //[System.Serializable]
    //public class Boundary
    //{
    //    public float xMin, xMax, zMin, zMax, playerHeight;
    //}

    //public Boundary boundary;

    // Use this for initialization
    void Start ()
    {
        gameObject.transform.rotation = rotate;

        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        //anim.SetFloat("MoveX", playerRigidBody.velocity.x);
        //anim.SetFloat("MoveZ", playerRigidBody.velocity.z);
        

    }



    // Update is called once per frame
    void FixedUpdate ()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 move = new Vector3(x, 0, z);

        //rb.position = new Vector3
        //(
        //    Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //    boundary.playerHeight,
        //    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        //);

        rb.velocity = move * speed;
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
        {
            anim.SetBool("IsMoving", true);

            if (Input.GetAxis("Horizontal") > 0.1)
            {
                //spriteRenderer.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.1)
            {
                //spriteRenderer.flipX = false;
            }
        }
        else
            anim.SetBool("IsMoving", false);
        
    }
}
