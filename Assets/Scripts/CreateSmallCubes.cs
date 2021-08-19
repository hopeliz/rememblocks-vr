using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSmallCubes : MonoBehaviour
{
    [Header("Script References")]
    public GameController gameController;

    [Header("Objects")]
    public GameObject smallCubePrefab;
    public GameObject parentCube;
    public GameObject[] smallCubes;

    [Header("Offsets")]
    public float cubeOffsetFar = 0.375F;
    public float cubeOffsetNear = 0.125F;

    [Header("Controls")]
    public bool hasAppeared = false;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    
    void Update()
    {
        if (!hasAppeared)       // Appears once at a time
        {
            // Turn parent cube on
            parentCube.SetActive(true);

            for (int j = 0; j < gameController.numberOfSetsOfSmallCubes; j++)
            {
                CreateCubes();
            }

            hasAppeared = true;
        }

    }

    public void CreateCubes()
    {
        for (int i = 0; i < 64; i++)
        {
            GameObject smallCube = Instantiate(smallCubePrefab);
            smallCube.name = smallCubePrefab.name + i;
            smallCube.transform.SetParent(parentCube.transform);
           
            if (i < 16)
            {
                smallCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cubeOffsetFar);

                if (i < 4)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 0:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 1:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 2:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 3:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 3 && i < 8)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 4:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 5:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 6:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 7:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 7 && i < 12)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 8:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 9:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 10:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 11:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 11 && i < 16)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 12:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 13:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 14:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 15:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }


            }

            if (i > 15 && i < 32)
            {
                smallCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cubeOffsetNear);

                if (i > 15 && i < 20)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 16:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 17:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 18:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 19:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 19 && i < 24)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 20:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 21:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 22:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 23:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 23 && i < 28)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 24:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 25:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 26:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 27:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 27 && i < 32)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 28:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 29:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 30:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 31:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }
            }

            if (i > 31 && i < 48)
            {
                smallCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cubeOffsetNear);

                if (i > 31 && i < 36)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 32:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 33:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 34:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 35:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 35 && i < 40)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 36:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 37:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 38:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 39:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 39 && i < 44)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 40:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 41:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 42:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 43:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 43 && i < 48)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 44:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 45:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 46:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 47:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }
            }

            if (i > 47 && i < 64)
            {
                smallCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cubeOffsetFar);

                if (i > 47 && i < 52)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 48:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 49:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 50:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 51:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 51 && i < 56)
                {
                    smallCube.transform.position = new Vector3(transform.position.x + cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 52:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 53:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 54:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 55:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 55 && i < 60)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetNear, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 56:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 57:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 58:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 59:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }

                if (i > 59 && i < 64)
                {
                    smallCube.transform.position = new Vector3(transform.position.x - cubeOffsetFar, transform.position.y, smallCube.transform.position.z);

                    switch (i)
                    {
                        case 60:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        case 61:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y + cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 62:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetNear, smallCube.transform.position.z);
                            break;
                        case 63:
                            smallCube.transform.position = new Vector3(smallCube.transform.position.x, transform.position.y - cubeOffsetFar, smallCube.transform.position.z);
                            break;
                        default:
                            print("Error");
                            break;
                    }
                }
            }

            if (gameController.addGravity)
            {
                smallCube.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
