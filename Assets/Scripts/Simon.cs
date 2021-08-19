using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;
    public FailureControl failureControl;
    public SoundControl soundControl;
    public ResetGame resetGame;
    public EndPlay endPlay;
    public Level1 level1;
    public Level2 level2;
    public Level3 level3;
    public Level4 level4;
    public Level5 level5;
    public Level6 level6;
    public Level7 level7;
    public Level8 level8;

    [Header("Game Controls")]
    public int level = 1;
    public int stage = 0;
    // Stage 0: Suspended Play
    // Stage 1: Prompting
    // Stage 2: Listening
    public int round = 0;
    public int lightNumber = 0;
    public int colorClicked = -1;
    public int levelThreshold = 3;
    public bool gameOver = true;    // Allows for delay
    public bool lightIsOn = false;
    public bool paused = false;
    public bool isListening = false;
    public bool isGray = false;
    public bool grayComplete = false;
    public int score = 0;
    public bool countingDown = false;
    public bool leftPlayTrigger = true;
    public bool isWaitingForPlay = false;

    [Header("Game Control Values")]
    public int currentColorNumber;
    public int colorNumberPlayed;
    public int correctColorNumber;
    public int lastColorNumber;

    [Header("Timing")]
    public float promptLength = 1.0F;
    public float pauseLength = 0.5F;
    public float listeningLength = 7;
    public float grayLength = 2;
    public float waitingForPlayLength = 5;
    public float speedModifier = 0.05F;
    public float speedThreshold = 0.25F;

    [Header("Countdowns")]
    public float promptCountdown;
    public float pauseCountdown;
    public float listenCountdown;
    public float grayCountdown;
    public float gameStartCountdown;
    public AudioSource countdown1;
    public AudioSource countdown2;
    public float waitingForPlayCountdown;

    [Header("Level Control")]
    public GameObject uiReplay;
    public GameObject uiNextLevel;

    [Header("Navigation Objects")]
    public GameObject playGameNav;

    [Header("Materials")]
    public Material grayMaterial;

    [Header("Cubes In Play")]
    public GameObject[] cubePrefabs;
    public GameObject[] cubesInPlay;

    [Header("Light Control")]
    public List<int> lightPrompts;

    void Start()
    {
        currentColorNumber = -1;    // Default color number;
        colorNumberPlayed = -1;     // No number has been played
        correctColorNumber = -1;    // No expected number
        stage = 0;                  // Stage 0: Suspended Play

        // Set Countdowns
        if (round > 3)
        {
            pauseCountdown = speedThreshold;
            promptCountdown = speedThreshold;
        }
        else
        {
            promptCountdown = promptLength - (round * speedModifier);
            pauseCountdown = pauseLength - -(round * speedModifier);
        }

        listenCountdown = listeningLength;
        grayCountdown = grayLength;

        // Create Light Prompt List
        AddColorToList();
    }

    void Update()
    {
        #region Countdown Control block

        // When starting, there is a 3-second countdown
        if (countingDown)
        {
            gameStartCountdown -= Time.deltaTime;

            if (gameController.showCountdownNumbers)
            {
                if (Mathf.CeilToInt(gameStartCountdown) == 4)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.three;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.three;
                }

                if (Mathf.CeilToInt(gameStartCountdown) == 3)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.two;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.two;
                }

                if (Mathf.CeilToInt(gameStartCountdown) == 2)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.one;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.one;
                }

                if (Mathf.CeilToInt(gameStartCountdown) == 1)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.go;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.go;
                }
            }

            if (gameStartCountdown < 0)
            {
                countingDown = false;
                gameController.northWall.GetComponent<Renderer>().material = gameController.blankWall;
                gameController.southWall.GetComponent<Renderer>().material = gameController.blankWall;

                SetupLevel(level, false);

                // Start prompting stage
                stage = 1;
                gameOver = false;
            }
        }
        // If the light is on, start prompt countdown
        if (lightIsOn) { promptCountdown -= Time.deltaTime; }

        // If it's pausing during prompts, start pause countdown
        if (paused) { pauseCountdown -= Time.deltaTime; }

        // When pause ends, reset and turn off the pause
        if (pauseCountdown < 0)
        {
            paused = false;

            if (round > 3)
            {
                pauseCountdown = speedThreshold;
            }
            else
            {
                pauseCountdown = pauseLength - (round * speedModifier);
            }
        }

        // If it's gray, start gray countdown
        if (isGray) {

            // Delay if paused;
            if (!paused)
            {
                print("Gray");
                grayCountdown -= Time.deltaTime;

                if (gameController.grayOut)
                {
                    for (int i = 0; i < cubesInPlay.Length; i++)
                    {
                        cubesInPlay[i].GetComponent<Renderer>().material = grayMaterial;
                    }

                    soundControl.betweenRoundSound.enabled = true;
                    soundControl.betweenRoundSound.mute = false;
                }
            }
        }

        // When gray time ends, reset and turn off gray
        if (grayCountdown < 0)
        {
            if (gameController.grayOut)
            {
                for (int i = 0; i < cubesInPlay.Length; i++)
                {
                    cubesInPlay[i].GetComponent<Renderer>().material = cubesInPlay[i].GetComponent<CubeBehavior>().unselectedMaterial;
                }

                soundControl.betweenRoundSound.mute = true;
                soundControl.betweenRoundSound.enabled = false;
            }

            isGray = false;
            grayCountdown = grayLength;
            grayComplete = true;
            print("Gray Complete");

            // Update score and round UI
            if (gameController.showRound)
            {
                gameController.roundNumberText.text = (round + 1).ToString();
            }

            // Pause before first light
            paused = true;
        }

        // If in listening mode, start listening countdown
        if (isListening) { listenCountdown -= Time.deltaTime; }

        // If waiting for play, start waiting for play countdown
        if (isWaitingForPlay) { waitingForPlayCountdown -= Time.deltaTime; }

        #endregion Countdown Control block

        #region Gameplay block

        // If game is in play
        if (!gameOver)
        {
            // Stage 1: Prompting stage
            if (stage == 1)
            {
                // During the prompting stage, the color number played should be defaulted to -1 
                colorNumberPlayed = -1;
                print("Sarting tage 1 - Round: " + round);

                // Before the first light, gray should run once
                if (lightNumber == 0 && !grayComplete)
                {
                    isGray = true;
                }

                // When gray time is complete

                if (grayComplete)
                {
                    // Should only run when not paused
                    if (!paused)
                    {
                        // While prompt is counting down
                        if (promptCountdown > 0)
                        {
                            // Turn proper light on
                            print("Prompting: " + cubesInPlay[lightPrompts[lightNumber]].GetComponent<Transform>());
                            cubesInPlay[lightPrompts[lightNumber]].GetComponent<CubeBehavior>().isSelected = true;
                            //cubesInPlay[lightPrompts[lightNumber]].GetComponent<CubeBehavior>().LightOn();
                            lightIsOn = true;
                        }
                        else
                        {
                            // Otherwise, turn the proper light off
                            //cubesInPlay[lightPrompts[lightNumber]].GetComponent<CubeBehavior>().LightOff();
                            lightIsOn = false;

                            if (round > 3)
                            {
                                promptCountdown = speedThreshold;
                            }
                            else
                            {
                                promptCountdown = promptLength - (round * speedModifier);     // Reset prompt
                            }

                            // If the light number is fewer than the number of rounds
                            if (lightNumber < round)
                            {
                                lightNumber++;      // Move on to next light
                                paused = true;      // Turn on pause before next prompt
                            }
                            else
                            {
                                // If the light number is equal to (or for some reason more) the number of rounds

                                stage = 2;          // Move on to listening stage
                                lightNumber = 0;    // Reset light number
                            }
                        }
                    }
                }
            }

            // Stage 2: Listening Stage
            if (stage == 2)
            {
                print("Stage 2 - Round: " + round);

                // Listen stage starts immediately, but this might change to having a sound or color effect to "pass" it to the player
                isListening = true;

                // Set correct number
                correctColorNumber = lightPrompts[lightNumber];

                print("Listening for " + correctColorNumber);
                print("Light Number: " + lightNumber);
                print("Round: " + round);

                // If color number played has changed from default
                if (colorNumberPlayed > -1)
                {
                    // CORRECT RESPONSE
                    // If the color played matches the correct color
                    if (colorNumberPlayed == correctColorNumber)
                    {
                        // If the light number is less than the round number
                        if (lightNumber < round)
                        {
                            print("listening again");
                            
                            colorNumberPlayed = -1;             // Reset color number played to default
                            listenCountdown = listeningLength;  // Reset listening time
                            lightNumber++;                      // Increase light number
                            print("light number increase");
                        } else
                        {
                            // If light number is equal to (or somehow more than) the round

                            // Add a color to the prompt list
                            AddColorToList();

                            round++;                            // Go to next round
                            score++;                            // Increase score
                            lightNumber = 0;                    // Reset light number
                            colorNumberPlayed = -1;             // Reset color number played to default
                            stage = 1;

                            // Set pause between rounds to be longer and start pause
                            pauseCountdown = pauseLength * 2;
                            paused = true;

                            // No longer listening
                            isListening = false;
                            listenCountdown = listeningLength;  // Reset listening length

                            // Set it up to show gray time
                            grayComplete = false;

                            print("Next Round!");
                        }
                    }
                    else
                    {
                        // FAIL FROM INCORRECT RESPONSE
                        print("Wrong answer! You lose!");
                        print("Looking for " + correctColorNumber);

                        LoseGame();
                    }
                }
                
                // If listen countdown runs out
                if (listenCountdown < 0 && !gameOver)
                {
                    // FAIL FROM RUNNING OUT OF TIME
                    print("Time's up! You lose!");
                    gameController.northWall.GetComponent<Renderer>().material = gameController.gameOverTimeUp;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.gameOverTimeUp;

                    LoseGame();
                }
            }
        }

        #endregion Gameplay block

        // Below is temporary test code to work outside of VR
        /*
        if (Input.GetKeyDown(KeyCode.M) && gameOver)
        {
            StartGame();
        }

        if (!gameOver && stage == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                cubesInPlay[0].GetComponent<CubeBehavior>().isSelected = true;
                cubesInPlay[0].GetComponent<CubeBehavior>().timesSelected++;
                colorNumberPlayed = 0;
                lastColorNumber = 0;

                if (gameController.cubeMovementType == 2)
                {
                    cubesInPlay[0].GetComponent<CubeBehavior>().destinationPosition = new Vector3(cubesInPlay[0].GetComponent<CubeBehavior>().cubeOriginalPosition.x, cubesInPlay[0].GetComponent<CubeBehavior>().cubeOriginalPosition.y + (cubesInPlay[0].GetComponent<CubeBehavior>().timesSelected * 0.25F), cubesInPlay[0].GetComponent<CubeBehavior>().cubeOriginalPosition.z);
                }

                if (level == 8)
                {
                    cubesInPlay[0].GetComponent<CubeBehavior>().movement = Mathf.CeilToInt(Random.Range(0, 5.99F));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                cubesInPlay[1].GetComponent<CubeBehavior>().isSelected = true;
                cubesInPlay[1].GetComponent<CubeBehavior>().timesSelected++;
                colorNumberPlayed = 1;
                lastColorNumber = 1;

                if (gameController.cubeMovementType == 2)
                {
                    cubesInPlay[1].GetComponent<CubeBehavior>().destinationPosition = new Vector3(cubesInPlay[1].GetComponent<CubeBehavior>().cubeOriginalPosition.x, cubesInPlay[1].GetComponent<CubeBehavior>().cubeOriginalPosition.y + (cubesInPlay[1].GetComponent<CubeBehavior>().timesSelected * 0.25F), cubesInPlay[1].GetComponent<CubeBehavior>().cubeOriginalPosition.z);
                }

                if (level == 8)
                {
                    cubesInPlay[1].GetComponent<CubeBehavior>().movement = Mathf.CeilToInt(Random.Range(0, 5.99F));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                cubesInPlay[2].GetComponent<CubeBehavior>().isSelected = true;
                cubesInPlay[2].GetComponent<CubeBehavior>().timesSelected++;
                colorNumberPlayed = 2;
                lastColorNumber = 2;

                if (gameController.cubeMovementType == 2)
                {
                    cubesInPlay[2].GetComponent<CubeBehavior>().destinationPosition = new Vector3(cubesInPlay[2].GetComponent<CubeBehavior>().cubeOriginalPosition.x, cubesInPlay[2].GetComponent<CubeBehavior>().cubeOriginalPosition.y + (cubesInPlay[2].GetComponent<CubeBehavior>().timesSelected * 0.25F), cubesInPlay[2].GetComponent<CubeBehavior>().cubeOriginalPosition.z);
                }

                if (level == 8)
                {
                    cubesInPlay[2].GetComponent<CubeBehavior>().movement = Mathf.CeilToInt(Random.Range(0, 5.99F));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                cubesInPlay[3].GetComponent<CubeBehavior>().isSelected = true;
                cubesInPlay[3].GetComponent<CubeBehavior>().timesSelected++;
                colorNumberPlayed = 3;
                lastColorNumber = 3;

                if (gameController.cubeMovementType == 2)
                {
                    cubesInPlay[3].GetComponent<CubeBehavior>().destinationPosition = new Vector3(cubesInPlay[3].GetComponent<CubeBehavior>().cubeOriginalPosition.x, cubesInPlay[3].GetComponent<CubeBehavior>().cubeOriginalPosition.y + (cubesInPlay[3].GetComponent<CubeBehavior>().timesSelected * 0.25F), cubesInPlay[3].GetComponent<CubeBehavior>().cubeOriginalPosition.z);
                }

                if (level == 8)
                {
                    cubesInPlay[3].GetComponent<CubeBehavior>().movement = Mathf.CeilToInt(Random.Range(0, 5.99F));
                }
            }
        }

        if (gameOver)
        {
            // N-Key override for testing without VR
            if (Input.GetKeyDown(KeyCode.N))
            {
                for (int i = 0; i < cubesInPlay.Length; i++)
                {
                    Destroy(cubePrefabs[i]);
                }

                // Test level
                SetupLevel(4, true);
            }
        }
        */
    }

    public void AddColorToList()
    {
        lightPrompts.Add(Mathf.FloorToInt(Random.Range(0, 3.99F)));
    }

    public void StartGame()
    {
        for (int i = 0; i < cubesInPlay.Length; i++)
        {
            Destroy(cubePrefabs[i]);
        }

        gameStartCountdown = 4;
        countingDown = true;
        endPlay.isInFreePlay = false;
        endPlay.freePlayCountdown = endPlay.freePlayTime;

        playGameNav.GetComponent<NavigationBehavior>().objectToDisappear.SetActive(false);

        leftPlayTrigger = false;

        GetComponent<PlayFailSound>().enabled = false;
    }

    public void LoseGame()
    {
        gameController.failed = true;

        isListening = false;
        gameOver = true;
        print("Final Score: " + (lightPrompts.Count - 1));
        colorNumberPlayed = -1;

        // Scoring
        gameController.scoreTextObject.SetActive(true);
        gameController.scoreNumberTextObject.SetActive(true);
        gameController.highScoreTextObject.SetActive(true);
        gameController.highScoreNumberTextObject.SetActive(true);

        gameController.scoreNumberText.text = (gameController.currentScore + score).ToString();
        
        if ((gameController.pastScore + score) > gameController.highScore)
        {
            gameController.highScore = gameController.pastScore + score;
            gameController.highScoreNumberText.text = (gameController.highScore).ToString();
        }

        // Environment
        gameController.cubeMovementType = 3;
        gameController.showNav = true;

        isWaitingForPlay = true;
        endPlay.isInFreePlay = true;

        if (round > levelThreshold)
        {
            if (gameController.northWall.GetComponent<Renderer>().material != gameController.gameOverTimeUp)
            {
                if (level == 8)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.gameOverYouWin;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.gameOverYouWin;
                }

                if (level < 8)
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.gameOverNext;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.gameOverNext;
                }
            }

            playGameNav.GetComponent<NavigationBehavior>().objectToAppear = uiNextLevel;
            uiNextLevel.SetActive(false);
        }
        else
        {
            if (gameController.northWall.GetComponent<Renderer>().material != gameController.gameOverTimeUp)
            {

                if (listenCountdown < 0)
                {
                    // FAIL FROM RUNNING OUT OF TIME
                    print("Time's up! You lose!");
                    gameController.northWall.GetComponent<Renderer>().material = gameController.gameOverTimeUp;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.gameOverTimeUp;
                }
                else
                {
                    gameController.northWall.GetComponent<Renderer>().material = gameController.gameOverClose;
                    gameController.southWall.GetComponent<Renderer>().material = gameController.gameOverClose;
                }


            }

            playGameNav.GetComponent<NavigationBehavior>().objectToAppear = uiReplay;
            uiReplay.SetActive(false);
        }

        playGameNav.GetComponent<NavigationBehavior>().objectToDisappear.SetActive(true);
        playGameNav.GetComponent<NavigationBehavior>().objectToAppear.SetActive(false);
    }

    public void SetupLevel(int level, bool replay)
    {
        gameController.failType = Mathf.FloorToInt(Random.Range(0, 4.99F));

        if (level == 1)
        {
            for (int i = 0; i < level1.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level1.cubePrefabs[i], level1.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level1.cubePositions[i].x, level1.cubePositions[i].y + gameController.tooHighModifier, level1.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level1.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level1.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level1.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level1.movementType;
        }

        if (level == 2)
        {
            for (int i = 0; i < level2.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level2.cubePrefabs[i], level2.parent.transform);
                
                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level2.cubePositions[i].x, level2.cubePositions[i].y + gameController.tooHighModifier, level2.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level2.cubePositions[i];
                }
                
                cubePrefabs[i].transform.rotation = level2.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level2.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level2.movementType;
        }

        if (level == 3)
        {
            for (int i = 0; i < level3.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level3.cubePrefabs[i], level3.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level3.cubePositions[i].x, level3.cubePositions[i].y + gameController.tooHighModifier, level3.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level3.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level3.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level3.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level3.movementType;
        }

        if (level == 4)
        {
            for (int i = 0; i < level4.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level4.cubePrefabs[i], level4.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level4.cubePositions[i].x, level4.cubePositions[i].y + gameController.tooLowModifier, level4.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level4.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level4.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level4.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level4.movementType;
        }

        if (level == 5)
        {
            for (int i = 0; i < level5.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level5.cubePrefabs[i], level5.parent.transform);
                
                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level5.cubePositions[i].x, level5.cubePositions[i].y + gameController.tooHighModifier, level5.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level5.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level5.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level5.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level5.movementType;
        }

        if (level == 6)
        {
            for (int i = 0; i < level6.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level6.cubePrefabs[i], level6.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level6.cubePositions[i].x, level6.cubePositions[i].y + gameController.tooLowModifier, level6.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level6.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level6.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level6.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level6.movementType;
        }

        if (level == 7)
        {
            for (int i = 0; i < level7.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level7.cubePrefabs[i], level7.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level7.cubePositions[i].x, level7.cubePositions[i].y + gameController.tooHighModifier, level7.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level7.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level7.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level7.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level7.movementType;
        }

        if (level == 8)
        {
            for (int i = 0; i < level8.cubePrefabs.Count; i++)
            {
                // Create Cubes and place them
                cubePrefabs[i] = Instantiate(level8.cubePrefabs[i], level8.parent.transform);

                if (gameController.accessibilityOn)
                {
                    cubePrefabs[i].transform.position = new Vector3(level8.cubePositions[i].x, level8.cubePositions[i].y + gameController.tooHighModifier, level8.cubePositions[i].z);
                }
                else
                {
                    cubePrefabs[i].transform.position = level8.cubePositions[i];
                }

                cubePrefabs[i].transform.rotation = level8.cubeRotations[i];
                cubePrefabs[i].transform.eulerAngles = level8.cubeAngles[i];

                // Set Big Cubes / Cubes in play
                cubesInPlay[i] = cubePrefabs[i].transform.GetChild(0).gameObject;
            }

            // Update Failure Control
            for (int i = 0; i < cubesInPlay.Length; i++)
            {
                // Grab the cubes in play
                failureControl.bigCubes[i] = cubesInPlay[i];
                failureControl.parentCubes[i] = cubePrefabs[i].transform.GetChild(1).gameObject;
            }

            gameController.cubeMovementType = level8.movementType;
        }

        // RESET EVERYTHING
        gameOver = true;    // Allows for delay
        lightIsOn = false;
        paused = false;
        isListening = false;
        isGray = false;
        grayComplete = false;

        round = 0;
        lightNumber = 0;
        colorClicked = -1;

        currentColorNumber = -1;    // Reset to default color number;
        colorNumberPlayed = -1;     // Reset played number
        correctColorNumber = -1;    // Reset expected number
        stage = 0;                  // Reset to Stage 0: Suspended Play

        // Reset Countdowns
        if (round > 3)
        {
            pauseCountdown = speedThreshold;
            promptCountdown = speedThreshold;
        }
        else
        {
            promptCountdown = promptLength - (round * speedModifier);
            pauseCountdown = pauseLength - (round * speedModifier);
        }

        listenCountdown = listeningLength;
        grayCountdown = grayLength;
        
        // Clear light prompt list
        lightPrompts.Clear();

        // Create New Light Prompt List
        AddColorToList();
    }
}
