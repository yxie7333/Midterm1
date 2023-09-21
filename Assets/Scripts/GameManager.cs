using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        GameOver,
        PlayerWon
    }

    public GameState currentState = GameState.Playing;

    private void Awake()
    {
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
        Time.timeScale = 1; // Reset time scale before changing scene
        currentState = GameState.GameOver;
        //SceneManager.LoadScene("LossScene");
        StartCoroutine(WaitAndRestartGame(2f));
    }

    public void PlayerWon()
    {
        Time.timeScale = 1; // Reset time scale before changing scene
        currentState = GameState.PlayerWon;
        //SceneManager.LoadScene("WinScene");
        StartCoroutine(WaitAndRestartGame(2f));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
        currentState = GameState.Playing;
    }

    private IEnumerator WaitAndRestartGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartGame();
    }

    // Singleton Instance
    public static GameManager instance;
}
