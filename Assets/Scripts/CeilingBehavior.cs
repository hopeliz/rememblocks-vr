using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingBehavior : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;

    [Header("Materials")]
    public Material transparentMaterial;
    public Material clearMaterial;
    
    void Update()
    {
        if (gameController.ceilingActive) {
            gameObject.GetComponent<Renderer>().material = transparentMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = clearMaterial;
            gameController.ceilingSolid = false;
        }
        
        if (gameController.ceilingSolid) { gameObject.GetComponent<Collider>().enabled = true; }
        else { gameObject.GetComponent<Collider>().enabled = false; }
    }
}
