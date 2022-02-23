using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    //tally the # of obstacles hit
    public int count = 0;
    //has the palyer started driving?
    public bool started = false;
    //name the fmod evenet emitter for music
    FMODUnity.StudioEventEmitter ambiMusic;

    //store vehicle location to calculate velocity
    private Vector3 whereItWas;
    //variable to calculate how fast the vehicle is moving
    public float VehicleVelocity = 0.0f;

    // variable for current RPM value
    public float runRPM = 0.0f;

    private void Awake()
    {
        ambiMusic = GetComponent<FMODUnity.StudioEventEmitter>();
    }

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

        /* //TUB CODE
        // calculate and store current RPM
        runRPM = VehicleVelocity * 100;
        //create emitter object
        var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        // apply RPM values to RPM parameter in FMOD
        emitter.SetParameter("RPM", runRPM);
        
        //checks input if we are turning
        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
        {
            emitter.SetParameter("Turn", 10f);
        }
        if (!Input.GetButton("Horizontal"))
        {
            emitter.SetParameter("Turn", 0f);
        }
        */
        //player interactions
        if (!started)
        {
            //test keybaord for intiial palyer input on any direcitonal key
            if(/*horizontalInput != 0 ||*/ forwardInput != 0)
            {
                //player is now driving
                started = true;
                //apply velocity value to the impact parameter and send to FMOD event
                ambiMusic.SetParameter("driving", 1);
            }
        }
    }

    //when this game object intersectsa  collider with 'is trigger' checked,
    //store a reference to that collider in a variable named other
    private void OnTriggerEnter(Collider other)
    {
        // if the game object we intersect has obstacle assigned
        if (other.gameObject.CompareTag("Obstacle"))
        {
            //increase the variable by one
            count++;
        }
        if(count == 5)
        {
            //play end tag if all hit
            ambiMusic.SetParameter("obstacles", 1);
        }
    }
}
