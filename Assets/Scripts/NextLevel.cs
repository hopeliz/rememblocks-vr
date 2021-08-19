using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Simon simon;
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
            simon.level++;
            simon.StartGame();
            parentObject.SetActive(false);
            gameController.showNav = false;
        }
    }
}
