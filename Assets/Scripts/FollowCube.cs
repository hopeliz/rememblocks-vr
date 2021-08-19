using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    public GameObject cubeToFollow;

    void Update()
    {
        if (cubeToFollow != null)
        {
            transform.position = cubeToFollow.transform.position;
            transform.rotation = cubeToFollow.transform.rotation;
        }
    }
}
