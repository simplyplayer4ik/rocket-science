using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10, 10, 10);
    [SerializeField] float period = 3;
    const float tau = Mathf.PI * 2;
    float cycles;
    float rawSinWave;
    float movementFactor;
    Vector3 startPos;
    Vector3 offset;
    void Start()
    {
        startPos = transform.position;
    }

   
    void Update()
    {
        if(period <= 0)
        {
            return;
        }
        cycles = Time.time/period;
        rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2 + 0.5f;
        offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
