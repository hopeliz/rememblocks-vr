using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCredits : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // **** Uncomment if menu is reinstated ****
            //gameController.section = 2;
        }
    }
}
