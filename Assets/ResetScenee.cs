using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScenee : MonoBehaviour
{
    void RestartScene() //EVENT
    {
        SceneManager.LoadScene(2); //2 is the game;
    }
}