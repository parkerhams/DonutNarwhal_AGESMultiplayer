using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    [SerializeField] private GameObject obj;
    [SerializeField] private string layerName;

	// Use this for initialization
	void Start () {
        //only visible to certain layers - any given script can target certain layers, 
        //like maybe a function only gets called if the object colliding with in on the layer mask
        //Like donuts on each four layers - instea dof four strings, just one layermask
        obj.layer = LayerMask.NameToLayer(layerName);

        //Do this on OnEnable: you would have a GameManager to set the player number, and you would say to 
        //concatenate that layer number onto the string - ex. if you had layers Narwhal1,Narwhal2, etc. then 
        //set that object's layer to whatever number.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
