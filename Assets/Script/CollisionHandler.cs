using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Score;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    Movement movement;
    AudioSource audioSources;

    bool isTransitioning = false;
    bool collisionDisabled= false;
    void Start() 
    {
        movement = GetComponent<Movement>();
        audioSources= GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            if (collisionDisabled)
            {
                Debug.Log("Collision is disabled");
                return;
            }
            Debug.Log("Collision is enabled");
            return;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled){return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's safe to touch!");
                break;
            case "Finish":
                Debug.Log("You reach the Finish Line!");   
                StartNextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("You've added extra fuel!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticle.Play();
        audioSources.Stop();
        audioSources.PlayOneShot(crashSound);
        movement.enabled = false;
        Invoke("ReloadLevel",2f);
    }

    void StartNextLevelSequence()
    {
        isTransitioning = true;
        successParticle.Play();
        audioSources.Stop();
        audioSources.PlayOneShot(successSound);
        movement.enabled = false;
        Invoke("NextLevel",nextLevelDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Your spaceship crashed!");
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxScene = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = currentSceneIndex+1;
        if (nextSceneIndex == maxScene) 
        {
            nextSceneIndex = 0;
        }
        Debug.Log("level "+ nextSceneIndex+1);
        SceneManager.LoadScene(nextSceneIndex);
    }
}
