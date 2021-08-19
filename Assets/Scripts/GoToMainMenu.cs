using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            // **** Uncomment if menu is reinstated ****
            //gameController.section = 0;
        }
    }
}
