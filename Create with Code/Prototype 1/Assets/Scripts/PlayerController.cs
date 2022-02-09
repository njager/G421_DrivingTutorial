using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    //store vehicle location to calculate velocity
    private Vector3 whereItWas;
    //variable to calculate how fast the vehicle is moving
    public float VehicleVelocity = 0.0f;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Calculate vehicle velocity using transform
        VehicleVelocity = (((transform.position - whereItWas).magnitude) / Time.deltaTime) / 2;
        //Update vehicle position
        whereItWas = transform.position;

        //MOve the vehicle forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //Rotate the vehicle based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        
    }
}
