using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float engThrust = 100f;
   [SerializeField] AudioClip mainEngine;
   [SerializeField] AudioClip explosion;
   [SerializeField] AudioClip completStage;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Trascending };
    State state = State.Alive;
   
	
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //ignore the function OnCollision

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing 
                break;

            case "Finish": //reach the goal and touch the object with "Finish" TAG
                SuccessSequece();
                break;

            default:
                DeathSequence();
                break;

        }
    }

    private void SuccessSequece()
    {
        state = State.Trascending;
        audioSource.PlayOneShot(completStage);
        Invoke("LoadNextLevel", 1f);//parameterise this time
    }
   
    private void DeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        Invoke("ReturnFirstLevel", 1.5F);
    }
  
    private void LoadNextLevel() //load next level when you rich the landing pad 
    {
        SceneManager.LoadScene(1);
    }

    private void ReturnFirstLevel() // load first level when you died 
    {
        SceneManager.LoadScene(0);
    }

    

    // Update is called once per frame
    void Update () {
        if (state == State.Alive)
        {
            RespondThrustImput();
            RespondRotateImput();
        }
    }

    private void RespondThrustImput()
    {
        if (Input.GetKey(KeyCode.Space))//When space is pessed vector3 get UP force. And engine sound FX start play. And need State to be Alive for works
        {
            ApllyThrust();
        }
        else
        {
            audioSource.Stop();
        }
        
    }

    private void ApllyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * engThrust);
        if (!audioSource.isPlaying) //Start to play audio when the space key is pressed and the audio is not play yet
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondRotateImput()
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
