using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int crashes = 0;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's safe to touch!");
                break;
            case "Finish":
                Debug.Log("You reach the Finish Line!");
                Debug.Log("You've crashed "+crashes+" times!");
                break;
            case "Fuel":
                Debug.Log("You've added extra fuel!");
                break;
            default:
                Debug.Log("Your spaceship crashed!");
                crashes++;
                SceneManager.LoadScene("SandBox");
                break;
        }
    }
}
