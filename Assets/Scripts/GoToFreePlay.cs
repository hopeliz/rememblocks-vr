using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFreePlay : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // **** Uncomment if free play is reinstated ****
            //gameController.section = 3;
        }
    }
}
