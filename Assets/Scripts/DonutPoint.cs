using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutPoint : MonoBehaviour {

    //will need reference to the Junkbot script so it can check whether it's Alive or not
    //All I want this to do is when the weakpoint collides with an object tagged "Weapon," it sets Junkbot.isAlive to "false"
    //Actually handle what it means to be dead in the junkbot script itself
    //This way, this script can be attached to any object

    private NarwhalMoveAndTurn parentNarwhal;
    [SerializeField]
    AudioSource hitSource;
	// Use this for initialization
	void Start ()
    {
        parentNarwhal = GetComponentInParent<NarwhalMoveAndTurn>();
        hitSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && parentNarwhal.isAlive)
        {
            Debug.Log("The Narwhal is dead");
            if (parentNarwhal.isAlive = false)
                parentNarwhal.gameObject.SetActive(false);
            hitSource.Play();
        }
    }
}
