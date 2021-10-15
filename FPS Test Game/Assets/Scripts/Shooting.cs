using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    //public float range = 100f;
    public float force = 1f;
    public int magSize = 30;
    public int ammoCount = 0;
    //references
    public ParticleSystem flash;
    public Camera fpsCam;
    public GameObject impact;

    //fire rate values
    public float fireRate = 10f;
    private float nextToFire = 0f;
    
    //private float nextToReload = 0f;

    // Update is called once per frame
    void Update()
    {
        //if mouse1 pressed fire the shoota
        //also limits fire presses based on rate of fire values
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && ammoCount > 0)
        {
            ammoCount--;
            nextToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        //check if user presses r and reloads mag if mag is not empty and they are not already shooting
        if (Input.GetKeyDown(KeyCode.R) == true && Time.time >= nextToFire && ammoCount < magSize)
        {

            //nextToReload = Time.time + 1f;
            ammoCount = magSize;
        }
    }

    public void Shoot()
    {
        Debug.Log(ammoCount + "/" + magSize);
        flash.Play();
        RaycastHit hit;
        //shoots ray out and returns true if ray hits an object
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {

            //if targeted object has rigidbody apply a force 
            //(can add more if statements for targeting different things such as a damage handler)
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            //leave effect where ray hit object and delete that object 1 second later
            GameObject bullet = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bullet, 1f);
        }
    }
}
