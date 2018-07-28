using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
   
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();                
    }

    private void ProcessInput()
    {

        if (Input.GetKey(KeyCode.Space)) //When space is pessed vector3 get UP force. And engine sound FX start play
        {
            rigidBody.AddRelativeForce(Vector3.up);
            PlayRocketEngineFX();
        }
        else //Stop rocket sounf FX
        {
             audioSource.Stop();
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 75);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 75);
        }       
    }

    private void PlayRocketEngineFX()
    {
        if (!audioSource.isPlaying) //Start to play audio when the space key is pressed and the audio is not play yet
        {
            audioSource.Play();
        }       
    }
}
