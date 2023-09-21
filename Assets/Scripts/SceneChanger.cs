using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceChanger : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
    
       // SceneManager.LoadScene("All");

    }

}
