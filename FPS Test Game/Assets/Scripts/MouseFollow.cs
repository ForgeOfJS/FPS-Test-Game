using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    //set target game object (player)
    public Transform playerBody;
    //set default rotation and mouse sens values
    public float yRotation = 0f;
    public float mouseSens = 100f;

    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //aquire horizontal and vertical mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //adjust y rotation by vertical movement
        yRotation -= mouseY;

        //only allow y rotation to look up and down by 90 degrees
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        //rotate the camera 
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        //rotate the game object (player)
        playerBody.Rotate(Vector3.up * mouseX);



    }
}
