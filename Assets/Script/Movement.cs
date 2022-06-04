using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidBody;
    AudioSource audioSources;
    [SerializeField] float thrustForce =1000f;
    [SerializeField] float rotationSpeed = 300f;
    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody= GetComponent<Rigidbody>();
        audioSources= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotations();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rocketRigidBody.AddRelativeForce(Vector3.up*thrustForce*Time.deltaTime);
            if(!audioSources.isPlaying)
            {
                audioSources.Play();
            }
            
        } 
        else 
        {
            audioSources.Stop();
        }
        
    }
    
    void ProcessRotations()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float f)
    {
        rocketRigidBody.freezeRotation = true; //Freezing the rotation so we could manually rotate
        transform.Rotate(Vector3.forward * f *Time.deltaTime );
        rocketRigidBody.freezeRotation = false; // unfreezing so physics system could takeover
    }
}
