using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyAudioSourceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.progress < 3)DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.progress > 2) Destroy(gameObject);
    }
}
