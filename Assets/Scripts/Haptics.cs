using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Haptics : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;

    public float pulseDelay = 0.25F;
    public float pulseDuration = 0.25F;
    public float pulseFrequency = 15;
    public float pulseAplitude = 75;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);

        print("Pulse - " + source.ToString());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Big Cube")
        {
            print(gameObject.GetComponentInParent<GameObject>().name);

            if (gameObject.GetComponentInParent<GameObject>().name == "Controller (left)")
            {
                Pulse(pulseDuration, pulseFrequency, pulseAplitude, SteamVR_Input_Sources.LeftHand);
            }

            if (gameObject.GetComponentInParent<GameObject>().name == "Controller (right)")
            {
                Pulse(pulseDuration, pulseFrequency, pulseAplitude, SteamVR_Input_Sources.RightHand);
            }
        }
    }
}
