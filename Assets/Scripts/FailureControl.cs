using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureControl : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;
    public Simon simon;

    [Header("Objects")]
    public GameObject[] bigCubes;
    public GameObject[] parentCubes;

    public int failType;
    // Fail Type 0: Add gravity to big blocks
    // Fail Type 1: Turn into blocks
    // Fail Type 2: Turn into blocks with gravity
    // Fail Type 3: Explode into blocks
    // Fail Type 4: Explode into blocks with gravity
    // Fail Type 5: Color glitch
    // Fail Type 6: Floor drop

    [Header("Values")]
    public float soundCountdown;
    public float maxSoundLength;
    
    void Update()
    {
        if (gameController.failed)
        {
            // Turn off sound
            simon.cubePrefabs[simon.lastColorNumber].GetComponent<MainCubeControl>().cubeSound.mute = true;

            switch (gameController.failType)
            {
                case 0:
                    for (int i = 0; i < bigCubes.Length; i++)
                    {
                        gameController.lerpIntoPlace = false;
                        bigCubes[i].GetComponent<Rigidbody>().useGravity = true;
                    }
                    break;
                case 1:
                    GetComponent<PlayFailSound>().enabled = true;
                    BreakIntoSmallCubes();
                    gameController.addGravity = false;
                    break;
                case 2:
                    GetComponent<PlayFailSound>().enabled = true;
                    gameController.fadeOutOnImpact = false;
                    BreakIntoSmallCubes();
                    gameController.addGravity = true;
                    break;
                case 3:
                    GetComponent<PlayFailSound>().enabled = true;
                    BreakIntoSmallCubes();
                    gameController.numberOfSetsOfSmallCubes = 4;
                    gameController.addGravity = false;
                    break;
                case 4:
                    GetComponent<PlayFailSound>().enabled = true;
                    BreakIntoSmallCubes();
                    gameController.numberOfSetsOfSmallCubes = 4;
                    gameController.fadeOutOnImpact = false;
                    gameController.addGravity = true;
                    break;
            }

            gameController.failed = false;
            simon.colorNumberPlayed = -1;
        }
    }

    public void BreakIntoSmallCubes()
    {
        if (gameController.justCorrectCube)
        {
            bigCubes[simon.correctColorNumber].SetActive(false);
            parentCubes[simon.correctColorNumber].GetComponent<CreateSmallCubes>().enabled = true;
            parentCubes[simon.correctColorNumber].GetComponent<CreateSmallCubes>().hasAppeared = false;
            parentCubes[simon.correctColorNumber].GetComponent<MeshRenderer>().enabled = false;
        }

        if (gameController.justIncorrectCube)
        {
            bigCubes[simon.lastColorNumber].SetActive(false);
            parentCubes[simon.lastColorNumber].GetComponent<CreateSmallCubes>().enabled = true;
            parentCubes[simon.lastColorNumber].GetComponent<CreateSmallCubes>().hasAppeared = false;
            parentCubes[simon.lastColorNumber].GetComponent<MeshRenderer>().enabled = false;
        }

        if (!gameController.justIncorrectCube && !gameController.justCorrectCube)
        {
            for (int i = 0; i < bigCubes.Length; i++)
            {
                bigCubes[i].SetActive(false);
                parentCubes[i].GetComponent<CreateSmallCubes>().enabled = true;
                parentCubes[i].GetComponent<CreateSmallCubes>().hasAppeared = false;
                parentCubes[i].GetComponent<CreateSmallCubes>().GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
