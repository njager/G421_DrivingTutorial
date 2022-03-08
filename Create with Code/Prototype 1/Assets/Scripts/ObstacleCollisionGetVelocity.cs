using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollisionGetVelocity : MonoBehaviour
{
    // variable for velocity
    float hitVelocity;
    GameObject vehicle;


    // Start is called before the first frame update
    void Start()
    {
        vehicle = GameObject.Find("Vehicle");
    }

    
    void OnTriggerEnter()
    {
        //access player controllcer script
        PlayerController playerController = vehicle.GetComponent<PlayerController>();
        //set hit velocity to velocity from veicle
        hitVelocity = playerController.VehicleVelocity;
        print(hitVelocity);

        //access the FMOD event emitter that plays the sound effect
        var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        //update the impact parameter with the velocity value
        emitter.SetParameter("impact", hitVelocity);
    }
}
