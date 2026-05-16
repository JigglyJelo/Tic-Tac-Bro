using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButtonScript : MonoBehaviour{
    private AudioSource birds;
    private AudioSource happy;
    private AudioSource rain;
    private float timer = 0;
    // Start is called before the first frame update
    void Start(){
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
        timer += Time.deltaTime;
        birds = GameObject.Find("birds").GetComponent<AudioSource>();
        happy = GameObject.Find("happy").GetComponent<AudioSource>();
        rain = GameObject.Find("rain").GetComponent<AudioSource>();
    }

    void ButtonClicked(){
        if(GameManager.songsIndex != 3) GameManager.songsIndex++;
        else GameManager.songsIndex = 0;
        playSong();
    }

    private void playSong(){
        switch(GameManager.songsIndex){
            case 0:
                happy.Stop();
                rain.Stop();
                birds.Stop();
                break;
            case 1:
                birds.Play();
                rain.Stop();
                happy.Stop();
                break;
            case 2: 
                happy.Play();
                birds.Stop();
                rain.Stop();
                break;
            case 3:
                rain.Play();
                happy.Stop();
                birds.Stop();
                break;
        }
    }
}


