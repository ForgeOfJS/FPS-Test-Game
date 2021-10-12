using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    //public float range = 100f;
    public float force = 1f;

    //references
    public ParticleSystem flash;
    public Camera fpsCam;
    public GameObject impact;

    //fire rate values
    public float fireRate = 2f;
    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        //if mouse-1 pressed fire
        //also limits fire presses based on rate of fire values
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        flash.Play();
        RaycastHit hit;
        //shoots ray out and returns true if ray hits an object
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            //if targeted object has rigidbody apply a force 
            //(can add more aspects to this such as a damage handler)
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            //leave effect where ray hit object
            GameObject bullet = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(bullet);
        }
    }
}
