using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280,720,false);
        Application.targetFrameRate = 60;
        Destroy(GameObject.Find("birds"));
        Destroy(GameObject.Find("happy"));
        Destroy(GameObject.Find("rain"));
    }
}
