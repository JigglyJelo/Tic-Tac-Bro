using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButtonScript : MonoBehaviour
{
    private AudioSource birds;
    private AudioSource happy;
    private AudioSource rain;

    void Start(){
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked(){
        Destroy(GameObject.Find("birds"));
        Destroy(GameObject.Find("happy"));
        Destroy(GameObject.Find("rain"));
        SceneManager.LoadScene("Main Menu");
    }
}
