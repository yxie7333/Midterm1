using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance.

    public enum GameState
    {
        Playing,
        GameOver,
        PlayerWon
    }

    public GameState currentState = GameState.Playing;

    private void Awake()
    {
        // Singleton pattern implementation.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // When the game is over.
    public void GameOver()
    {
        currentState = GameState.GameOver;
        
        Debug.Log("Game Over!");

        // RestartGame();
    }

    public void PlayerWon()
    {
        currentState = GameState.PlayerWon;
        Debug.Log("Player Won!");

        // RestartGame();
    }

    // Function to restart the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
