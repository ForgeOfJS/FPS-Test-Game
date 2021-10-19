using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    //public float range = 100f;
    public float force = 1f;
    public float maxCharge = 30f;
    private float currCharge = 0f;
    //references
    public ParticleSystem flash;
    public Camera fpsCam;
    public GameObject impact;

    //fire rate values
    public float fireRate = 10f;
    private float nextToFire = 0f;
    
    void Update()
    {
        Debug.Log((int)currCharge + "/" + maxCharge);
        //if mouse1 pressed fire
        //also limits fire presses based on rate of fire values and current charge
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && currCharge < maxCharge)
        {
            currCharge += 1f;
            nextToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        //cool down the gun after gun stops firing
        if (currCharge > 0 && Time.time >= nextToFire)
        {
            currCharge -= 3f * Time.deltaTime;
        }
        //check if user presses r and reloads mag if mag is not empty and they are not already shooting
        //if (Input.GetKeyDown(KeyCode.R) == true && Time.time >= nextToFire && currCharge < maxCharge)
        //{
           // currCharge = maxCharge;
        //}

    }

    public void Shoot()
    {
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
