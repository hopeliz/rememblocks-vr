using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSection : MonoBehaviour
{
    public GameController gameController;

    public int section;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            // **** Uncomment if menu is reinstated ****
            //gameController.section = section;
        }
    }
}
