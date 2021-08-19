using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFailSound : MonoBehaviour
{
    public AudioSource failSound;
    public AudioSource failSound1;
    public AudioSource failSound2;
    
    void Start()
    {
        int soundNumber = Mathf.FloorToInt(Random.Range(0, 2.99F));

        switch (soundNumber)
        {
            case 0:
                failSound.Play();
                break;
            case 1:
                failSound1.Play();
                break;
            case 2:
                failSound2.Play();
                break;
            default:
                failSound.Play();
                break;
        }
    }
}
