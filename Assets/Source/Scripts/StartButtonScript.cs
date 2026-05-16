using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour{
    void Start(){
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
        if(PlayerPrefs.GetInt("beaten", 0) == 0 && gameObject.name.Equals("EndlessButton")){
            Destroy(gameObject);
        } 
    }

    public void ButtonClicked(){
        GridScript.aiWins = 0;
        GridScript.playerWins = 0;
        if(gameObject.name.Equals("StartButton")) SceneManager.LoadScene("Cutscene");
        else{
            GameManager.songsIndex = 0;
            SceneManager.LoadScene("Game Scene");
        } 
    }
}
