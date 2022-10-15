using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject []cameras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //视角切换
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(!cameras[i].activeSelf);
            }
        }


    }
}
