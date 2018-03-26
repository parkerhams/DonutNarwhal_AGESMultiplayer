using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
   [SerializeField]
    float damage = 10f;
    // Time a projectile should exist before despawning
    [SerializeField]
    float lifetime = 3f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
