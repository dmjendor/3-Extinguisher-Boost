using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody extinguisherBody;
    AudioSource thrust;

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 60f;


    // Start is called before the first frame update
    void Start()
    {
        extinguisherBody = GetComponent<Rigidbody>();
        thrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly": // no action safe to hit
                print("OK");
                break;
            case "Fuel": // increase fuel
                print("Fuel");
                break;
            default: // kill the player
                print("Dead");
                break;
        }            
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            extinguisherBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!thrust.isPlaying)
            {
                thrust.Play();
            }
        }
        else
        {
            thrust.Stop();
        }
    }
    private void Rotate()
    {

        float rotationByFrame = rcsThrust * Time.deltaTime;
        extinguisherBody.freezeRotation = true; // pause physics control
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * rotationByFrame);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * rotationByFrame);
        }
        extinguisherBody.freezeRotation = false; // resume physics control
    }

}
