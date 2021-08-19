using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;
    public Simon simon;

    [Header("Object References")]
    public GameObject parentObject;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        simon = FindObjectOfType<Simon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            simon.StartGame();
            parentObject.SetActive(false);
            gameController.showNav = false;
            print("This should start");
        }

        print("does this work?");
    }
}
