using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForVideo : MonoBehaviour
{
    public GameObject[] bigCubes;
    public GameObject[] parentCubes;

    public bool splode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            splode = true;
        }

        if (splode)
        {
            GetComponent<GameController>().failed = true;
            GetComponent<FailureControl>().failType = 3;

            //cube1.GetComponent<CreateSmallCubes>().enabled = true;
            //cube2.GetComponent<CreateSmallCubes>().enabled = true;
            //cube3.GetComponent<CreateSmallCubes>().enabled = true;
            //cube4.GetComponent<CreateSmallCubes>().enabled = true;
            for (int i = 0; i < bigCubes.Length; i++)
            {
                bigCubes[i].SetActive(false);
                parentCubes[i].GetComponent<CreateSmallCubes>().enabled = true;
                parentCubes[i].GetComponent<CreateSmallCubes>().hasAppeared = false;
                parentCubes[i].GetComponent<CreateSmallCubes>().GetComponent<MeshRenderer>().enabled = false;
            }
            splode = false;
        }
    }
}
