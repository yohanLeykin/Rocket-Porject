using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayTime = 1f;
    [SerializeField] AudioClip onCrash;
    [SerializeField] AudioClip onSuccess;
    [SerializeField] ParticleSystem onCrashPartical;
    [SerializeField] ParticleSystem onSuccessPartical;  
    AudioSource myAudio;
    bool isTransitioning = false;
    bool collisionDisable = false;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
   void OnCollisionEnter(Collision other)
   {
        if(isTransitioning || collisionDisable){ return; }
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
    PlaySoundAndEffects(onCrash,onCrashPartical);
    GetComponent<Movment>().enabled = false;
    Invoke("ReloadLevel",delayTime);
   }
   
   void StartNextLevelSequence()
   {
    isTransitioning = true;
    PlaySoundAndEffects(onSuccess,onSuccessPartical);
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

   void PlaySoundAndEffects(AudioClip sceneAudio,ParticleSystem scenePatrical)
   {
        myAudio.Stop();
        myAudio.PlayOneShot(sceneAudio);
        scenePatrical.Play();
   }
}
