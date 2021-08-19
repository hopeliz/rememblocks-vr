using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlay : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;
    public Simon simon;

    [Header("Objects")]
    public GameObject parent;
    public List<GameObject> cubePrefabs;

    [Header("Values")]
    public List<Vector3> cubePositions;
    public List<Quaternion> cubeRotations;
    public List<Vector3> cubeAngles;
    public int movementType = 2;

    [Header("UI Control")]
    public GameObject settings;
    public GameObject credits;
    public GameObject playGame;
    public GameObject freePlay;
    public GameObject mainMenuWall;

    [Header("Accessibility")]
    public int type = 0;
    // Type 0: Too High
    // Type 1: Too Low
    
    void Update()
    {
        // **** Uncomment if menu is reinstated ****
        /*
        if (gameController.section == 3)
        {
            // Turn on Main Menu items
            // mainMenuWall.SetActive(true);

            // Turn off other UI
            // settings.SetActive(false);
            // credits.SetActive(false);
            playGame.SetActive(false);
            // freePlay.SetActive(false);

        }
        */
    }
}
