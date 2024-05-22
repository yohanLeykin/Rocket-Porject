using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayTime = 1f;
    [SerializeField] AudioClip onCrash;
    [SerializeField] AudioClip onSuccess; 
    AudioSource myAudio;
    bool isTransitioning = false;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
   void OnCollisionEnter(Collision other)
   {
        if(isTransitioning){ return; }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Object!");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel Object!");
                break;
            default:
                StartCrushSequence();
                break;            
        }
   }

   void StartCrushSequence()
   {
    isTransitioning = true;
    myAudio.Stop();
    myAudio.PlayOneShot(onCrash);
    GetComponent<Movment>().enabled = false;
    Invoke("ReloadLevel",delayTime);
   }
   
   void StartNextLevelSequence()
   {
    isTransitioning = true;
    myAudio.Stop();
    myAudio.PlayOneShot(onSuccess);
    GetComponent<Movment>().enabled = false;
    Invoke("LoadNextLevel",delayTime);
   }

   void ReloadLevel()
   {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
   }

   void LoadNextLevel()
   {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
   }
}
