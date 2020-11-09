using UnityEngine;
using UnityEngine.SceneManagement; // Library required for changing scenes

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Unlock the cursor and make visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        // Loads the next scene in our build queue
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Closes down the program
        Debug.Log("Quit");

        Application.Quit();
    }
}