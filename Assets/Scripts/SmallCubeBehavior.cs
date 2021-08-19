using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCubeBehavior : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            if (gameController.fadeOutOnImpact)
            {
                gameObject.GetComponent<FadeAway>().enabled = true;
                gameObject.GetComponent<FadeAway>().fadingOut = true;
            }
            
        }
    }
}
