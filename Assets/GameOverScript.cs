using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverScreen;  // The Game Over UI screen to show
    private bool isGameOver = false;

    // This method is called when the player dies
    public void GameOver()
    {
        if (isGameOver) return;  // Prevent multiple calls to GameOver
        isGameOver = true;

        // Show the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    // This method can be called by the Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }
}
