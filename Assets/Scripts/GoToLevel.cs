using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Simon simon;

    void Update()
    {
        // When 1 key is pressed, it starts a game on level 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartLevel(1);
            print("blah");
        }

        // When 2 key is pressed, it starts a game on level 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartLevel(2);
        }

        // When 3 key is pressed, it starts a game on level 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartLevel(3);
        }

        // When 4 key is pressed, it starts a game on level 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartLevel(4);
        }

        // When 5 key is pressed, it starts a game on level 5
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartLevel(5);
        }

        // When 6 key is pressed, it starts a game on level 6
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartLevel(6);
        }

        // When 7 key is pressed, it starts a game on level 7
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartLevel(7);
        }

        // When 8 key is pressed, it starts a game on level 8
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            StartLevel(8);
        }
    }

    public void StartLevel(int level)
    {
        simon.level = level;
    }
}
