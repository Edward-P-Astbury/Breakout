using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    private float restartDelay = 0.5f;

    public void EndGame()
    {
        // Ensures the game will only end once
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("You died");

            // Delay the function call
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}