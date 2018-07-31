using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float engThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;
   
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();        
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;

            case "Fuel":
                print("Fuel");
                break;

            default:
                print("dead");
                break;
                
        }
    }


    // Update is called once per frame
    void Update () {
        Thrust();
        Rotate();        
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //When space is pessed vector3 get UP force. And engine sound FX start play
        {
            rigidBody.AddRelativeForce(Vector3.up * engThrust);
            if (!audioSource.isPlaying) //Start to play audio when the space key is pressed and the audio is not play yet
            {
                audioSource.Play();
            }
        }
        else //Stop rocket sounf FX
        {
            audioSource.Stop();
        }      
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rcsThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rcsThrust);
        }

        rigidBody.freezeRotation = false;
    }


   
}
