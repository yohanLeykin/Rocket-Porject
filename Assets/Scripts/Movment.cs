using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField]float mainThrust = 100;
    [SerializeField]float rotationThrust = 100;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(mainEngine);
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
        }
        else
        {
                myAudio.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplayRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplayRotation(-rotationThrust);
        }
        
    }

    private void ApplayRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we could manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeazing rotation so the physics system can take over
    }
}
