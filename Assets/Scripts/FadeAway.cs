using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;

    [Header("Materials")]
    public Material defaultMaterial;
    public Material transparentMaterial;

    [Header("Object")]
    public GameObject piece;

    [Header("Fade Controls")]
    public float fadeOutTime;
    public float fadeOutSpeed = 10;
    public float fadeOutDelay = 5;
    public float fadeOutDelayTime;
    public bool fadingOut = false;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        fadeOutTime = fadeOutSpeed;
        fadeOutDelayTime = fadeOutDelay;
        piece = gameObject;
    }
    
    void Update()
    {
        // If fade out is triggered
        if (fadingOut)
        {
            fadeOutDelayTime -= Time.deltaTime * fadeOutDelay;

            if (fadeOutDelayTime < 0)
            {
                // Start countdown and fade
                fadeOutTime -= Time.deltaTime * fadeOutSpeed;

                transform.GetComponent<Renderer>().material.Lerp(transparentMaterial, defaultMaterial, fadeOutTime / 10);

                // Once faded
                if (fadeOutTime < 0)
                {
                    // Destroy if wanted or reset
                    if (gameController.removeAfterFadeOut)
                    {
                        if (piece != null)
                        {
                            Destroy(piece);
                        }
                    }
                    else
                    {
                        fadingOut = false;
                        fadeOutTime = fadeOutSpeed;
                        fadeOutDelayTime = fadeOutDelay;
                    }
                }
            }
        }
    }
}
