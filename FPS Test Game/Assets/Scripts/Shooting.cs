using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float force = 1f;
    public float maxCharge = 30f;
    public float chargeRate = 3f;
    public float maxCoolTime;
    public float range;

    private float currCharge = 0f;
    private bool isCooling = false;

    //references
    public ParticleSystem flash;
    public Camera fpsCam;
    public GameObject impact;

    //fire rate values
    public float fireRate = 10f;
    private float nextToFire = 0f;
    
    void Update()
    {
        //check if gun is currently reloading
        if (isCooling)
        {
            return;
        }
        //manual reload 
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(CoolDown());
            return;
        }
        
        Debug.Log((int)currCharge + "/" + maxCharge);
        //if mouse1 pressed fire
        //also limits fire presses based on rate of fire values and current charge
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && currCharge < maxCharge)
        {
            currCharge += 1f;

            //altering firerate based on the charge of the weapon
            //essentially the higher the currCharge the slower the firerate
            if (currCharge <= 20)
            {
                nextToFire = Time.time + 1f / fireRate;
            }
            else if (currCharge <= 25)
            {
                nextToFire = Time.time + 1.5f / fireRate;
            }
            else
            {
                nextToFire = Time.time + 2f / fireRate;
            }
            
            Shoot();

        }


        //cool down the gun after gun stops firing
        if (!Input.GetButton("Fire1") && currCharge > 0 && Time.time >= nextToFire)
        {
            currCharge -= chargeRate * Time.deltaTime;
        }

        if (currCharge >= maxCharge)
        {
            StartCoroutine(CoolDown());
            return;
        }
    }

    public void Shoot()
    {
        flash.Play();
        RaycastHit hit;
        //shoots ray out and returns true if ray hits an object
        //can also add effective range float value
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
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

    IEnumerator CoolDown()
    {
        isCooling = true;

        Debug.Log("Cooling Down...");
        yield return new WaitForSeconds(currCharge / maxCoolTime);
        //cool down the gun after gun stops firing
        currCharge = 0;

        isCooling = false;
    }
}
