using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;
    public MainCubeControl mainCubeControl;
    public Simon simon;

    [Header("Materials")]
    public Material unselectedMaterial;
    public Material selectedMaterial;
    public bool isSelected = false;
    public float lightDelay;
    public float lightDelayTime = 0.5F;
    public float touchDelayTime = 0.5F;
    public float promptDelayTime = 0.5F;

    [Header("Audio")]
    public AudioSource sound;

    [Header("Values")]
    public int colorNumber;
    public bool colorSent = false;

    [Header("Movement")]
    public Vector3 cubeOriginalPosition;
    public Quaternion cubeOriginalRotation;

    public Vector3 destinationPosition;
    public Quaternion destinationRotation;

    public bool isMoved = false;

    public float lerpTime = 2;
    public float lerpDelay = 1;

    public int timesSelected = 1;
    public int movement;

    [Header("Accessibility")]
    public bool lowCubes = false;
    public bool highCubes = true;
    public float heightModifier;
    
    void Start()
    {
        // Since on prefab that will be instantiated, find the game controller in the scene
        gameController = FindObjectOfType<GameController>();
        simon = FindObjectOfType<Simon>();

        // Get stating location and rotation
        cubeOriginalPosition = transform.position;
        cubeOriginalRotation = transform.rotation;

        // Set light delay
        lightDelay = lightDelayTime;

        // Get sound
        sound = mainCubeControl.cubeSound;

        // Set Time Selected
        timesSelected = 0;

        if (gameController.cubeMovementType == 2)
        {
            lerpTime = 1;
            lerpDelay = 0;
        }

        // Check for accessibility options
        if (gameController.accessibilityOn)
        {
            // Adjusts height
            if (lowCubes) { heightModifier = gameController.tooLowModifier; }
            if (highCubes) { heightModifier = gameController.tooHighModifier; }
            transform.position = new Vector3(transform.position.x, transform.position.y + heightModifier, transform.position.z);
        }
    }
    
    void Update()
    {
        if (gameController.cubeMovementType == 2)
        {
            if (simon.level == 7)
            {
                if (colorNumber == 0)
                {
                    destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y, cubeOriginalPosition.z + (timesSelected * 0.25F));
                }

                if (colorNumber == 1)
                {
                    destinationPosition = new Vector3(cubeOriginalPosition.x + (timesSelected * 0.25F), cubeOriginalPosition.y, cubeOriginalPosition.z);
                }

                if (colorNumber == 2)
                {
                    destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y, cubeOriginalPosition.z - (timesSelected * 0.25F));
                }

                if (colorNumber == 3)
                {
                    destinationPosition = new Vector3(cubeOriginalPosition.x - (timesSelected * 0.25F), cubeOriginalPosition.y, cubeOriginalPosition.z);
                }
            }
            else if (simon.level == 8) {

                switch (movement)
                {
                    case 0:
                        destinationPosition = new Vector3(cubeOriginalPosition.x + (timesSelected * 0.25F), cubeOriginalPosition.y, cubeOriginalPosition.z);
                        break;
                    case 1:
                        destinationPosition = new Vector3(cubeOriginalPosition.x - (timesSelected * 0.25F), cubeOriginalPosition.y, cubeOriginalPosition.z);
                        break;
                    case 2:
                        destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y + (timesSelected * 0.25F), cubeOriginalPosition.z);
                        break;
                    case 3:
                        destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y - (timesSelected * 0.25F), cubeOriginalPosition.z);
                        break;
                    case 4:
                        destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y, cubeOriginalPosition.z + (timesSelected * 0.25F));
                        break;
                    case 5:
                        destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y, cubeOriginalPosition.z - (timesSelected * 0.25F));
                        break;
                    default:
                        destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y, cubeOriginalPosition.z);
                        break;
                }
            }
            else
            {
                destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y + (timesSelected * 0.25F), cubeOriginalPosition.z);
            }
        }

        // When selected
        if (isSelected)
        {
            lightDelay -= Time.deltaTime;
            gameObject.GetComponent<Renderer>().material = selectedMaterial;
            sound.mute = false;
            print(gameObject.name + " is selected");
            
            if (lightDelay < 0)
            {
                isSelected = false;
                lightDelay = lightDelayTime;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = unselectedMaterial;
            sound.mute = true;
        }

        // Movement Type 0: No movement, controller goes through cube
        if (gameController.cubeMovementType == 0)
        {
            // Turn off moving
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        // Movement Types 1 & 2: Lerp back to a position
        if (gameController.cubeMovementType > 0 && gameController.cubeMovementType < 3)
        {
            // Make moveable
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            // if MT1, set destination postion to the original position
            if (gameController.cubeMovementType == 1)
            {
                destinationPosition = cubeOriginalPosition;
                destinationRotation = cubeOriginalRotation;
            }

            // If the cube is not where it should be
            if (transform.position != destinationPosition || transform.rotation != destinationRotation)
            {
                isMoved = true;
            }

            if (gameController.lerpIntoPlace)
            {
                if (isMoved)
                {
                    // Wait before moving back
                    lerpDelay -= Time.deltaTime;

                    if (lerpDelay < 0)
                    {
                        // Move back to original position
                        transform.position = Vector3.Lerp(transform.position, destinationPosition, Time.deltaTime);

                        // Remove velocity or it will never return to position
                        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                        // Move back to original position
                        transform.rotation = Quaternion.Lerp(transform.rotation, destinationRotation, Time.deltaTime);

                        if (transform.position == destinationPosition)
                        {
                            // Turn off moving
                            isMoved = false;

                            // Reset Lerp delay
                            if (gameController.cubeMovementType == 2)
                            {
                                lerpDelay = 0;
                            }
                            else
                            {
                                lerpDelay = 1;
                            }
                        }
                    }
                }
            }
        }

        // Movement Type 3: Anti-Gravity
        if (gameController.cubeMovementType == 3)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void LightOn()
    {
        print("light on!");
        transform.GetComponent<Renderer>().material = selectedMaterial;
        sound.mute = false;
    }

    public void LightOff()
    {
        transform.GetComponent<Renderer>().material = unselectedMaterial;
        sound.mute = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GameController")
        {
            // If not prompting
            if (simon.stage != 1)
            {
                isSelected = true;
                timesSelected++;
                lightDelay = touchDelayTime;
                simon.colorNumberPlayed = colorNumber;
                simon.lastColorNumber = colorNumber;


                if (gameController.cubeMovementType == 2)
                {
                    destinationPosition = new Vector3(cubeOriginalPosition.x, cubeOriginalPosition.y + (timesSelected * 0.25F), cubeOriginalPosition.z);
                }

                if (simon.level == 8)
                {
                    movement = Mathf.CeilToInt(Random.Range(0, 5.99F));
                }
            }
        }
    }
}
