using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAdjust : MonoBehaviour
{
    public GameController gameController;

    [Header("North Wall")]
    public Transform northWall;
    public Vector3 northWallOriginalPosition;
    public Vector3 northWallDestinationPosition;

    [Header("East Wall")]
    public Transform eastWall;
    public Vector3 eastWallOriginalPosition;
    public Vector3 eastWallDestinationPosition;

    [Header("South Wall")]
    public Transform southWall;
    public Vector3 southWallOriginalPosition;
    public Vector3 southWallDestinationPosition;

    [Header("West Wall")]
    public Transform westWall;
    public Vector3 westWallOriginalPosition;
    public Vector3 westWallDestinationPosition;
    public Transform westWallPanel;
    public Vector3 westWallPanelOriginalPosition;
    public Vector3 westWallPanelDestinationPosition;

    [Header("Modifiers")]
    public float sizeModifier = 0;
    public bool moveWall = false;
    public float moveSpeed = 2;
    
    void Start()
    {
        northWallOriginalPosition = northWall.position;
        eastWallOriginalPosition = eastWall.position;
        southWallOriginalPosition = southWall.position;
        westWallOriginalPosition = westWall.position;
        westWallPanelOriginalPosition = westWallPanel.position;
    }
    
    void Update()
    {
        northWall.position = new Vector3(northWallOriginalPosition.x, northWallOriginalPosition.y, northWallOriginalPosition.z + sizeModifier);
        eastWall.position = new Vector3(eastWallOriginalPosition.x + sizeModifier, eastWallOriginalPosition.y, eastWallOriginalPosition.z);
        southWall.position = new Vector3(southWallOriginalPosition.x, southWallOriginalPosition.y, southWallOriginalPosition.z - sizeModifier);
        westWall.position = new Vector3(westWallOriginalPosition.x - sizeModifier, westWallOriginalPosition.y, westWallOriginalPosition.z);
        westWallPanel.position = new Vector3(westWallPanelOriginalPosition.x - sizeModifier, westWallPanelOriginalPosition.y, westWallPanelOriginalPosition.z);
    }
}
