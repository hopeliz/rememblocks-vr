using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Cube Control")]
    public int cubeMovementType = 0;
    // Movement Type 0: No movement - controller goes through block
    // Movement Type 1: Cubes Lerp to last position
    // Movement Type 2: Cubes Lerp to a specified position
    // Movement Type 3: Anti-gravity

    public int failType = 0;
    // Fail Type 0: Add gravity to big blocks
    // Fail Type 1: Turn into blocks
    // Fail Type 2: Turn into blocks with gravity
    // Fail Type 3: Explode into blocks
    // Fail Type 4: Explode into blocks with gravity
    // Fail Type 5: Combo of gravity and anti-gravity blocks
    // Fail Type 6: Color glitch
    // Fail Type 7: Floor drop

    public bool failed = false;

    public bool fadeOutOnImpact = true;
    public bool removeAfterFadeOut = true;

    public bool justIncorrectCube = false;
    public bool justCorrectCube = false;
    public int numberOfSetsOfSmallCubes = 1;
    public bool lerpIntoPlace = true;

    public bool addGravity = false;

    public bool grayOut = true;

    [Header("Scoring")]
    public int currentScore = 0;
    public int pastScore = 0;
    public int highScore = 0;

    [Header("UI Platform")]
    public GameObject playGameNav;

    [Header("UI Wall Panel")]
    public GameObject gamePlayWallPanel;

    [Header("Game Play UI Info")]
    public GameObject roundTextObject;
    public GameObject roundNumberTextObject;
    public Text roundNumberText;

    public GameObject scoreTextObject;
    public GameObject scoreNumberTextObject;
    public Text scoreNumberText;

    public GameObject highScoreTextObject;
    public GameObject highScoreNumberTextObject;
    public Text highScoreNumberText;

    public GameObject countdownTextObject;
    public Text countdownText;
    public GameObject gameOverText;
    public GameObject levelTextObject;
    public Text levelText;

    [Header("Main Wall Design and Countdown")]
    public Material three;
    public Material two;
    public Material one;
    public Material go;
    public Material blankWall;
    public Material gameOverWall;
    public Material gameOverTimeUp;
    public Material gameOverClose;
    public Material gameOverNext;
    public Material thanksForPlaying;
    public Material gameOverYouWin;

    public GameObject northWall;
    public GameObject southWall;

    [Header("UI Apparence")]
    public bool showRound = false;
    public bool showNav = true;
    public bool showCountdownNumbers = false;
    public bool showCountdownBlocks = false;

    [Header("Ceiling Control")]
    public bool ceilingSolid = true;
    public bool ceilingActive = true;

    [Header("Accessibility")]
    public bool accessibilityOn = false;
    [Range(-0.43F,0)]
    public float tooHighModifier = 0;
    [Range(0.21F,0.43F)]
    public float tooLowModifier = 0;
    
    void Start()
    {
        failType = Mathf.FloorToInt(Random.Range(0, 4.99F));
    }
    
    void Update()
    {
        if (showRound)
        {
            roundTextObject.SetActive(true);
            roundNumberTextObject.SetActive(true);
        }
        else
        {
            roundTextObject.SetActive(false);
            roundNumberTextObject.SetActive(false);

        }
    }
}
