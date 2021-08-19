using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    void Update()
    {
        // When N key is pressed, it reloads the game
        // This is for when there is a new player or a bug
        if (Input.GetKeyDown(KeyCode.N))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
}
