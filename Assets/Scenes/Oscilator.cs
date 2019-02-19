using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[DisallowMultipleComponent]

public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movimentVector = new Vector3 (10f,10f,10f);
    [SerializeField] float period = 2f;

   float movimentFactor;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
          
        if (period <= Mathf.Epsilon) { return;}     
        
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;

        float rawSinwave = Mathf.Sin(cycles * tau);


        movimentFactor = rawSinwave /2f + 0.5f;
        Vector3 Offset = movimentVector * movimentFactor;
        transform.position = startingPos + Offset; 
        
    }
}
