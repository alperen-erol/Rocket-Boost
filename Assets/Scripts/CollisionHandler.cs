using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":

                break;
            case "Fuel":

                break;
            default:
                Debug.Log("Crash boom");
                break;
        }
    }
}
