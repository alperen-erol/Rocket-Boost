using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                NextLevel();
                break;
            case "Fuel":

                break;
            default:
                ReloadLevel();

                break;
        }
    }

    private static void NextLevel()
    {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneID == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneID = 0;
        }
        SceneManager.LoadScene(nextSceneID);
    }

    private static void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
