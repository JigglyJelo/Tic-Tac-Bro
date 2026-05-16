using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    private float clickTimer = 0;
    public GameObject LilBro;
    public GameObject LilBroNeutral;
    public GameObject LilBroSad;
    public GameObject Dad;
    public AudioSource mornin;
    public AudioSource slide;
    public AudioSource happy;
    public AudioSource rain;
    public static string[] dialog = {
        "Big Bro: Ah it's so peaceful out here I can't imagine anything that could ruin this",
        "Lil Bro: Hey bro",
        "Big Bro: What do you want?",
        "Lil Bro: Want to play a game?",
        "Big Bro: Not really",
        "Lil Bro: Don't worry I've got a great game to play",
        "",
        "Lil Bro: ...",
        "Big Bro: You good?",
        "Lil Bro: YOU CHEATED IM ALWAYS SUPPOSED TO WIN",
        "Lil Bro: DAD!",
        "Dad: Son your brother told me you were being mean to him",
        "Big Bro: We were just playing tic tac toe",
        "Dad: Listen son that clearly made him upset",
        "Dad: How about you just " + "let him win" + " so he stops crying",
        "Big Bro: Okay Dad I'll try",
        "Dad: Great! Now I'm going to get us some milk gotta go!",
        "Lil Bro: I want to play again but this time you better not cheat!",
        "Big Bro: What ever you say lil bro",
        "Lil Bro: Hurray! Don't worry bro I'll make it fair if we draw you can have the point cause I'm too good otherwise",
        "",
        "Lil Bro: Haha I told you I always win it's just too easy",
        "Big Bro: Uh huh",
        "Lil Bro: I'm going to go find someone else to beat now bye!",
        "The End",
        ""
    };
    public static int dialogIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(ButtonClicked);
        GetComponentInChildren<TMP_Text>().text = dialog[dialogIndex];
        if(dialogIndex == 7) LilBroNeutral.transform.position = new Vector3(0,-1.5f,0);
        if(GameManager.progress == 5) LilBro.transform.position = new Vector3(0,-1.5f,0);
        if(dialogIndex > 0) mornin.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        clickTimer += 1 * Time.deltaTime;
        if(dialogIndex < 7 && dialogIndex > 0 && LilBro.transform.position.y < -1.5f){
            LilBro.transform.Translate(new Vector3(0f, 5f * Time.deltaTime, 0f));
        } 
        if(dialogIndex >= 10 && dialogIndex < 16){
            LilBroSad.transform.Translate(new Vector3(20f * Time.deltaTime, 0, 0f));
        }
        else if(dialogIndex >= 23) LilBro.transform.Translate(new Vector3(10f * Time.deltaTime, 0f, 0f));

        switch(dialogIndex){
            case 1:
                mornin.Stop();
                if(LilBro.transform.position.y >= -1.5f) slide.Stop();
                break;
            case 2:
                LilBro.transform.position = new Vector3(0,-1.5f,0);
                slide.Stop();
                break;
            case 9:
                LilBroNeutral.transform.position = new Vector3(110,0,0);;
                LilBroSad.transform.position = new Vector3(0,-1.5f,0);
                break;
            case 11:
                if(Dad.transform.position.x > 0){
                    Dad.transform.Translate(new Vector3(-10f * Time.deltaTime, 0, 0f));
                }  
                break;
            case 15:
                LilBroSad.transform.position = new Vector3(15,-1.5f,0);
                break;
            case 16:
                Dad.transform.Translate(new Vector3(40f * Time.deltaTime, 0f, 0f));
                break;
            case 17:
                if(LilBroSad.transform.position.x > 0){
                    LilBroSad.transform.Translate(new Vector3(-15f  * Time.deltaTime, 0f, 0f));
                }
                break;
            case 19:
                LilBroSad.transform.position = new Vector3(100,-1.5f,0);
                LilBro.transform.position = new Vector3(0,-1.5f,0);
                break;
        }
    }

    public void ButtonClicked(){
        if(clickTimer >= 0.5){
            clickTimer = 0;
            dialogIndex++;
            switch(dialogIndex){
                case 1:
                    slide.Play();
                    GetComponentInChildren<TMP_Text>().text = dialog[dialogIndex];
                    break;
                case 3: 
                    happy.Play();
                    GetComponentInChildren<TMP_Text>().text = dialog[dialogIndex];
                    break;
                case 6:
                    GameManager.progress++;
                    SceneManager.LoadScene("Game Scene");
                    break;
                case 9:
                    rain.Play();
                    GetComponentInChildren<TMP_Text>().text = dialog[dialogIndex];
                    break;
                case 20:
                    GameManager.progress++;
                    SceneManager.LoadScene("Game Scene");
                    break;
                case 25:
                    GameManager.progress = 1;
                    dialogIndex = 0;
                    SceneManager.LoadScene("Main Menu");
                    break;
                default:
                    if(dialogIndex < dialog.Length -1){
                        GetComponentInChildren<TMP_Text>().text = dialog[dialogIndex];
                    }
                    break;
            }
        }
    }
}
