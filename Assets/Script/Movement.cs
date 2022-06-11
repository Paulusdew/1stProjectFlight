using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce =1000f;
    [SerializeField] float rotationSpeed = 300f;
    [SerializeField] AudioClip rocketEngineSound;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    Rigidbody rocketRigidBody;
    AudioSource audioSources;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotations()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rocketRigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSources.isPlaying)
        {
            audioSources.PlayOneShot(rocketEngineSound);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }

    private void StopThrusting()
    {
        audioSources.Stop();
        mainBoosterParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }

    private void StopRotation()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }

    void ApplyRotation(float f)
    {
        rocketRigidBody.freezeRotation = true; //Freezing the rotation so we could manually rotate
        transform.Rotate(Vector3.forward * f *Time.deltaTime );
        rocketRigidBody.freezeRotation = false; // unfreezing so physics system could takeover
    }

}
