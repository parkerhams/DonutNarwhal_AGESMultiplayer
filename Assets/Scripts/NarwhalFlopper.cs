using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarwhalFlopper : MonoBehaviour
{
    private Rigidbody moveBone;
	// Use this for initialization
	void Start ()
    {
        moveBone = GetComponent<Rigidbody>();
	}

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown("w"))
        {
            moveBone.AddForce(Vector3.forward * 1000);
        }

        if (Input.GetKeyDown("s"))
        {
            moveBone.AddForce(Vector3.back * 1000);
        }

        if (Input.GetKeyDown("d"))
        {
            moveBone.AddForce(Vector3.left * 1000);
        }

        if (Input.GetKeyDown("a"))
        {
            moveBone.AddForce(Vector3.right * 1000);
        }

        if (Input.GetKeyDown("space"))
        {
            moveBone.AddForce(Vector3.up * 10000);
        }
    }
	
	
}
