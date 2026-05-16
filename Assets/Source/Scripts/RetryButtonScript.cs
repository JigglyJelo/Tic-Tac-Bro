using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(){
        SceneManager.LoadSceneAsync("Game Scene");
        GridScript.aiWins = 0;
        GridScript.playerWins = 0;
    }

}
