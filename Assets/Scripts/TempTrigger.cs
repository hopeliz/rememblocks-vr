using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTrigger : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Simon simon;
    public FailureControl failureControl;

    [Header("Objects")]
    public GameObject[] bigCubes;
    public GameObject[] parentCubes;
    
    void Update()
    {
        // for testing
        //if (Input.GetKeyDown(KeyCode.B))
        /*
        if (gameController.failed)
        {
            failureControl.failType = 0;

            gameController.failed = false;
            simon.colorNumberPlayed = -1;
        }
        */
    }
}
