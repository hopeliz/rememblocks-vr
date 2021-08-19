using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon2 : MonoBehaviour
{
    [Header("References")]
    // Access GameController.cs
    public GameController gameController;

    [Header("Cubes In Play")]
    public GameObject[] cubesInPlay;

    [Header("Light Control")]
    public List<int> lightPrompts;

    [Header("Game Control Values")]
    public int colorNumberPlayed;
    public int correctColorNumber;

    [Header("Game Controls")]
    public bool gameOver = false;
    public int stage;
    // Stage 0: Suspended Play
    // Stage 1: Prompting
    // Stage 2: Listening
    public int round;
    public int lightNumber;

    [Header("Timing")]
    public float pauseLength;
    public float pauseCountdown;
    public bool isPaused = false;
    public bool pauseComplete = false;
    public float promptLength;
    public float promptCountdown;
    public bool isPrompting = false;
    public bool promptComplete = false;
    public float listenLength;
    public float listenCountdown;
    public bool isListening = false;
    

    void Start()
    {
        // Setup starting values
        pauseCountdown = pauseLength;
        promptCountdown = promptLength;
        listenCountdown = listenLength;
        stage = 0;
        round = 0;
        lightNumber = 0;

        // Add first color to list of prompts
        AddColorToList();

        // Selected color default
        colorNumberPlayed = -1;
        correctColorNumber = -1;
    }


    void Update()
    {
        // Pause control
        if (isPaused)
        {
            pauseCountdown -= Time.deltaTime;
        }

        if (isPrompting)
        {
            promptCountdown -= Time.deltaTime;
        }

        if (isListening)
        {
            listenCountdown -= Time.deltaTime;
        }

        // If in gameplay
        if (!gameOver)
        {
            // In prompting stage
            if (stage == 1)
            {
                print("Starting Stage 1");

                // Reset Color Number Played
                colorNumberPlayed = -1;

                // Reset correct number
                correctColorNumber = -1;
                print("Correct number updated to " + correctColorNumber);

                if (!pauseComplete)
                {
                    // Start countdown
                    isPaused = true;

                    if (pauseCountdown < 0)
                    {
                        // Stop and reset countdown
                        isPaused = false;
                        pauseComplete = true;
                        pauseCountdown = pauseLength;
                    }
                }

                for (int i = 0; i < lightPrompts.Count; i++)
                {
                    if (lightNumber <= round)
                    {
                        if (pauseComplete)
                        {
                            // Start countdown
                            isPrompting = true;
                            promptComplete = false;
                        }

                        if (promptCountdown > 0)
                        {
                            // Turn on light
                            cubesInPlay[lightPrompts[lightNumber]].GetComponent<Renderer>().material = cubesInPlay[lightPrompts[lightNumber]].GetComponent<CubeBehavior>().selectedMaterial;
                        }

                        if (promptCountdown < 0)
                        {
                            // Turn off light
                            cubesInPlay[lightPrompts[lightNumber]].GetComponent<Renderer>().material = cubesInPlay[lightPrompts[lightNumber]].GetComponent<CubeBehavior>().unselectedMaterial;

                            // Stop and reset countdown
                            isPrompting = false;
                            promptCountdown = promptLength;
                            promptComplete = true;

                            // Increase light number
                            lightNumber++;
                        }

                        if (promptComplete)
                        {
                            for (int j = 0; j < cubesInPlay.Length; j++)
                            {
                                cubesInPlay[j].GetComponent<Renderer>().material = cubesInPlay[j].GetComponent<CubeBehavior>().unselectedMaterial;
                            }

                            pauseComplete = false;
                            isPaused = true;
                        }
                    }

                    if (lightNumber > round)
                    {
                        // Reset light number
                        lightNumber = 0;
                        // Go to stage 2 (Listening)
                        stage = 2;
                        print("Going to State 2");
                    }
                }
            }

            // Listening Stage
            if (stage == 2)
            {
                isPaused = false;
                pauseCountdown = pauseLength;
                print("Starting State 2");

                isListening = true;

                if (isListening)
                {
                    // Set correct color number
                    correctColorNumber = lightPrompts[lightNumber];
                    print("Correct number set to " + correctColorNumber);
                    print("Light number: " + lightNumber);

                    // If a number is played
                    if (colorNumberPlayed > -1)
                    {
                        // If correct
                        if (colorNumberPlayed == correctColorNumber)
                        {
                            print("Correct!");

                            if (lightNumber < round)
                            {
                                print("Does this even run?");
                                // Reset listening countdown
                                listenCountdown = listenLength;

                                // Reset number played
                                colorNumberPlayed = -1;

                                // Go to next light number
                                lightNumber++;

                                // Set correct color number
                                correctColorNumber = lightPrompts[lightNumber];
                                print("Correct number set to " + correctColorNumber);
                                print("Light number: " + lightNumber);
                            }

                            // If last light prompt
                            else if (lightNumber == round)
                            {
                                print("blah");
                                // Go to next round
                                round++;

                                // Add light prompt
                                AddColorToList();

                                // Go back to stage 1
                                stage = 1;
                                pauseComplete = false;
                                print("Going to Stage 1");

                                // Stop and reset listening
                                isListening = false;
                                listenCountdown = listenLength;

                                // Reset played number
                                colorNumberPlayed = -1;
                            }
                            else
                            {
                                print("What?");
                            }
                        }
                        else
                        {
                            print("Wrong! Played: " + colorNumberPlayed + " Looking for: " + correctColorNumber);

                            // Stop and reset listening
                            isListening = false;
                            listenCountdown = listenLength;
                        }
                    }
                }
            }
        }

        // Below is temporary test code to work outside of VR

        if (Input.GetKeyDown(KeyCode.M))
        {
            // Start prompting stage
            stage = 1;
            gameOver = false;
        }

        if (!gameOver && stage == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                cubesInPlay[0].GetComponent<CubeBehavior>().isSelected = true;
                if (cubesInPlay[0].GetComponent<CubeBehavior>().colorSent)
                {
                    print("Color Sent!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                cubesInPlay[1].GetComponent<CubeBehavior>().isSelected = true;
                if (cubesInPlay[1].GetComponent<CubeBehavior>().colorSent)
                {
                    print("Color Sent!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                cubesInPlay[2].GetComponent<CubeBehavior>().isSelected = true;
                if (cubesInPlay[2].GetComponent<CubeBehavior>().colorSent)
                {
                    print("Color Sent!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                cubesInPlay[3].GetComponent<CubeBehavior>().isSelected = true;
                if (cubesInPlay[3].GetComponent<CubeBehavior>().colorSent)
                {
                    print("Color Sent!");
                }
            }
        }
    }

    public void AddColorToList()
    {
        lightPrompts.Add(Mathf.FloorToInt(Random.Range(0, 3.99F)));
    }
}
