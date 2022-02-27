using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

   [SerializeField] float rcsThrust = 100f;
   [SerializeField] float engThrust = 1000f;
   [SerializeField] AudioClip mainEngine;
   [SerializeField] AudioClip explosion;
   [SerializeField] AudioClip completStage;
   [SerializeField] ParticleSystem mainEngineParticles;
   [SerializeField] ParticleSystem explosionParticles;
   [SerializeField] ParticleSystem completStageParticles;
   [SerializeField] Boolean imortal = false;
   
    Rigidbody rigidBody;
    AudioSource audioSource;

    bool isTranscending = false;

    Gyroscope m_Gyro;


    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isTranscending) { return; } //ignore the function OnCollision

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
        isTranscending = true;
        audioSource.PlayOneShot(completStage);
        completStageParticles.Play();
        Invoke("LoadNextLevel", 1f);//parameterise this time
    }


    private void DeathSequence()
    {
        if (imortal == true) { return;}

        isTranscending = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        mainEngineParticles.Stop();
        explosionParticles.Play();
        Invoke("ReturnFirstLevel", 1.5F);
    }


    private void LoadNextLevel()
    {
        int currentSceneIdex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIdex + 1;
       
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        print(nextSceneIndex);
        print(SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(nextSceneIndex);
    }


    private void ReturnFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
     

    void Update ()
    {
        if (!isTranscending)
        {
            RespondThrustImput();
            RespondRotateImput();           
        }

        if (Debug.isDebugBuild)
        { 
        RespondTheBug();
        }

        Debug.Log(m_Gyro.rotationRate.x);
    }


    private void RespondTheBug()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            imortal = !imortal;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
      
    }

    private void RespondThrustImput()
    {
        if (Input.GetKey(KeyCode.Space))//When space is pessed vector3 get UP force. And engine sound FX start play. And need State to be Alive for works
        {
            ApllyThrust();
        }
        else if (Input.touchCount > 0)
        {
            ApllyThrust();
        }
        else
        {
            StopThrust();
        }

    }


    private void ApllyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * engThrust * Time.deltaTime);
        if (!audioSource.isPlaying) //Start to play audio when the space key is pressed and the audio is not play yet
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }


    private void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }


    private void RespondRotateImput()
    {      
        if (Input.GetKey(KeyCode.A))
        {
          ManualRotation(Time.deltaTime * rcsThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
           ManualRotation(-Time.deltaTime * rcsThrust);
        }

        GetGyroRotation(Time.deltaTime * rcsThrust);
        
    }

    private void ManualRotation(float calculateRotation)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * calculateRotation);
        rigidBody.freezeRotation = false;
    }

    private void GetGyroRotation(float calculateRotation)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * m_Gyro.rotationRate.z * calculateRotation);
        rigidBody.freezeRotation = false;
    }


}
