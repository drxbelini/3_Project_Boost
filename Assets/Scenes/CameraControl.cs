using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    Gyroscope m_Gyro;
    // Start is called before the first frame update
    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetGyroRotation(Time.deltaTime * rcsThrust);
    }
    private void GetGyroRotation(float calculateRotation)
    {
        //rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * m_Gyro.rotationRate.z * calculateRotation);
        //rigidBody.freezeRotation = false;
    }
}
