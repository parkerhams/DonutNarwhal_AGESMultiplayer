using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    LineRenderer line;
    public float laserWidth = .15f;
    [SerializeField]
    AudioSource shootingNoise;

    public ParticleSystem endEffect;
    Transform endEffectTransform;

    public Light shootLensFlare;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        shootingNoise = GetComponent<AudioSource>();
        shootLensFlare = GetComponent<Light>();

        endEffect = GetComponentInChildren<ParticleSystem>();
        if (endEffect)
           endEffectTransform = endEffect.transform;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {            
            //Just a safety net - we don't want more than one laser in case it keep running
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");

            shootingNoise.Play();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            line.enabled = false;
            shootingNoise.Stop();
            endEffect.Stop();
            shootLensFlare.enabled = false;
        }
    }

    //Coroutine -- make sure to stop
    IEnumerator FireLaser()
    {
        line.enabled = true;
        shootLensFlare.enabled = true;
        while(Input.GetButtonDown("Fire1"))
        {
            line.material.mainTextureOffset = new Vector2(0, Time.time);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                if(hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(transform.forward * 5, hit.point);
                }

                if (endEffect)
                {
                    endEffectTransform.position = hit.point;
                    if (!endEffect.isPlaying)
                        endEffect.Play();
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));

                if (endEffect)
                {
                    if (endEffect.isPlaying)
                        endEffect.Stop();
                }
            }

            yield return null;
        }
    }
}
