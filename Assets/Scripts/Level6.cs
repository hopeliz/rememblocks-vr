using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
{
    [Header("References")]
    public GameController gameController;
    public Simon simon;

    [Header("Objects")]
    public GameObject parent;
    public List<GameObject> cubePrefabs;

    [Header("Values")]
    public List<Vector3> cubePositions;
    public List<Quaternion> cubeRotations;
    public List<Vector3> cubeAngles;
    public int movementType = 2;

    [Header("Accessibility")]
    public int type = 1;
    // Type 0: Too High
    // Type 1: Too Low
}
