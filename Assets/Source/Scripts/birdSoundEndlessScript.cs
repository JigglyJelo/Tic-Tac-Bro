using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSoundEndlessScript : MonoBehaviour
{
    private static birdSoundEndlessScript instance;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}