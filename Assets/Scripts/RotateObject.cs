using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject player;
    public bool rotateSign = false;
    public float rotateSpeed = 2;
    
    void Update()
    {
        if (rotateSign)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(player.transform);
        }
    }
}
