using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int sceneLoadDelay = 2;
    private void OnCollisionEnter(Collision other)
    {
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
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", sceneLoadDelay);
    }

    private void HandleCrash()
    {
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
