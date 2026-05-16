using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridButtonScript : MonoBehaviour
{
    public AudioSource pencil;
    private int x;
    private int y;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
        x = int.Parse(this.name.Substring(6,1));
        y = int.Parse(this.name.Substring(7));
    }

    void Update()
    {
        char gridValue = GridScript.grid[x, y];
        TMP_Text buttonText = GetComponentInChildren<TMP_Text>();

        if (buttonText.text != gridValue.ToString())
        {
            buttonText.text = gridValue.ToString();
            //if(buttonText.text.Equals("X")) buttonText.color = new Color(255,0,0);
            //else if(buttonText.text.Equals("O")) buttonText.color = new Color(0,0,255);
        }
        if(!GridScript.winningWay.Equals("")){
            switch(GridScript.winningWay){
                case "row0":
                    if(x == 0) buttonText.color = new Color(255,0,0);
                    break;
                case "row1":
                    if(x == 1) buttonText.color = new Color(255,0,0);
                    break;
                case "row2":
                    if(x == 2) buttonText.color = new Color(255,0,0);
                    break;
                case "col0":
                    if(y == 0) buttonText.color = new Color(255,0,0);
                    break;
                case "col1":
                    if(y == 1) buttonText.color = new Color(255,0,0);
                    break;
                case "col2":
                    if(y == 2) buttonText.color = new Color(255,0,0);
                    break;
                case "diagDR":
                    if((x == 0 && y == 0) || (x == 1 && y == 1) || (x == 2 && y ==2)){
                        buttonText.color = new Color(255,0,0);
                    } 
                    break;
                case "diagUR":
                    if((x == 0 && y == 2) || (x == 1 && y == 1) || (x == 2 && y ==0)){
                        buttonText.color = new Color(255,0,0);
                    }
                    break;
            }
        }
    }

    public void ButtonClicked()
    {
        if (GridScript.turn % 2 == 0 && GridScript.victor.Equals(""))
        {
            if (GridScript.grid[x, y] == '_')
            {
                pencil.Play();
                GridScript.grid[x, y] = 'X';
                GridScript.turn++;
            }
        }
    }
}
