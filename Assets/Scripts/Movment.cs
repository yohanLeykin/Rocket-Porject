using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField]float mainThrust = 100;
    [SerializeField]float rotationThrust = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustPartical;  
    [SerializeField] ParticleSystem rightThrustPartical;  
    [SerializeField] ParticleSystem leftThrustPartical;  
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
            if(!mainThrustPartical.isPlaying)
            {
                mainThrustPartical.Play();
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            
        }
        else
        {
                myAudio.Stop();
                mainThrustPartical.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
             if(!leftThrustPartical.isPlaying)
            {
                leftThrustPartical.Play();
            }
            ApplayRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
             if(!rightThrustPartical.isPlaying)
            {
                rightThrustPartical.Play();
            }
            ApplayRotation(-rotationThrust);
        }
        else
        {
            leftThrustPartical.Stop();
            rightThrustPartical.Stop();
        }
        
    }

    private void ApplayRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we could manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeazing rotation so the physics system can take over
    }
}
