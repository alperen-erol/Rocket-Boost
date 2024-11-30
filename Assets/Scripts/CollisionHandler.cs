using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int sceneLoadDelay = 2;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    private bool isControllable = true;
    private bool isCollidable = true;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                HandleNextScene();
                break;
            default:
                HandleCrash();
                break;
        }
    }

    private void HandleNextScene()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(landSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", sceneLoadDelay);
    }

    private void HandleCrash()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        isControllable = false;
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
    }

    private void NextLevel()
    {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneID == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneID = 0;
        }
        SceneManager.LoadScene(nextSceneID);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
