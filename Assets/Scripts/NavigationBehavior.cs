using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBehavior : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Simon simon;

    [Header("Objects")]
    public GameObject objectToAppear;
    public GameObject objectToDisappear;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        simon = FindObjectOfType<Simon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameController.showNav && simon.leftPlayTrigger)
            {
                objectToDisappear.SetActive(false);
                objectToAppear.SetActive(true);

                // Reset Waiting for play
                simon.isWaitingForPlay = false;
                simon.waitingForPlayCountdown = simon.waitingForPlayLength;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameController.showNav && (simon.leftPlayTrigger || simon.waitingForPlayCountdown < 0))
            {
                objectToDisappear.SetActive(false);
                objectToAppear.SetActive(true);

                // Reset Waiting for play
                simon.isWaitingForPlay = false;
                simon.waitingForPlayCountdown = simon.waitingForPlayLength;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameController.showNav)
            {
                objectToDisappear.SetActive(true);
                objectToAppear.SetActive(false);
            }

            simon.leftPlayTrigger = true;

            // Reset Waiting for play
            simon.isWaitingForPlay = false;
            simon.waitingForPlayCountdown = simon.waitingForPlayLength;
        }
    }
}
