using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movmentVector;
    [SerializeField] [Range(0,5)]float movmentFactor;
    [SerializeField] float period = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movmentFactor = (rawSinWave + 1f) / 0.5f;
        Vector3 offset = movmentVector * movmentFactor;
        transform.position = startingPosition + offset;
    }
}
