using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public Text currentPellet;
    public Text totalPellet;
    public Player player;
    public GameObject winPanel;
    public GameObject lostPanel;


    private void Update()
    {
        currentPellet.text = "Current Pellet: " + player.currentPelletsCanUse.ToString();
        totalPellet.text = "Total Pellet: " + player.totalPelletsCollected.ToString();

        if (GameManager.instance.currentState == GameManager.GameState.PlayerWon)
        {
            winPanel.SetActive(true);
        }
        else
        {
            winPanel.SetActive(false);
        }
        if (GameManager.instance.currentState == GameManager.GameState.GameOver)
        {
            lostPanel.SetActive(true);
        }
        else
        {
            lostPanel.SetActive(false);
        }
    }



}
