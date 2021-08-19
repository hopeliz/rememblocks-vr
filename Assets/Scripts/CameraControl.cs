using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera cameraToRotate;

    // Start is called before the first frame update
    void Start()
    {
        cameraToRotate = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraToRotate.transform.eulerAngles += Vector3.right;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraToRotate.transform.eulerAngles += Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cameraToRotate.transform.eulerAngles += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraToRotate.transform.eulerAngles += Vector3.down;
        }


    }
}
