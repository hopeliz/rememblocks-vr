using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlay : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;

    [Header("Time Control")]
    public float freePlayTime = 30;
    public float freePlayCountdown;
    public bool isInFreePlay = false;

    void Start()
    {
        freePlayCountdown = freePlayTime;
    }

    void Update()
    {
        // Play ends when freeplay time is up
        if (isInFreePlay)
        {
            freePlayCountdown -= Time.deltaTime;
            if (freePlayCountdown <= 0)
            {
                EndPlayNow();
            }
        }

        // Ends play when Z is pressed
        // Helps keep control
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EndPlayNow();
        }

        // Escape key will exit the application
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void EndPlayNow()
    {
        SceneManager.LoadScene("Empty Scene", LoadSceneMode.Single);
    }
}
