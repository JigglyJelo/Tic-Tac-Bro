using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridScript : MonoBehaviour
{
    //Game Variables
    public string gameEnded;
    public static char[,] grid = new char[3,3];
    private float[,] theAiGrid;
    public static int turn;
    private TMP_Text gridText;
    //Ai Variables
    private char[,] aiGrid;
    private int selectionX;
    private int selectionY;
    public static string victor;
    public static int aiWins = 0;
    public static int playerWins = 0;
    public Button retry;
    public TMP_Text youText;
    public TMP_Text broText;
    public TMP_Text topText;
    public TMP_Text rageText;
    //public static bool restart = false;
    private bool roundFinished = false;
    private float roundOverTimer = 0f;
    public static string winningWay;
    public Button quit;
    public Button switchSong;

    // Start is called before the first frame update
    void Start()
    {
        winningWay = "";
        turn = 1;
        gameEnded  = "";
        victor = "";
        gridText = GetComponentInChildren<TMP_Text>();
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 3; j++){
                grid[i, j] = '_';
            }
        }
        youText.text = "Big Bro (X): " + playerWins;
        broText.text = "Lil Bro (O): " + aiWins;
        if(GameManager.progress == 2){
            youText.text = "";
            broText.text = "";
            topText.text = "Tic-Tac-Toe";
        }
        if(GameManager.progress == 1) topText.text = "Endless";
        if(GameManager.progress != 1){
            Destroy(quit.gameObject);
            Destroy(switchSong.gameObject);
        }
        rageText.text = "";
        aiTurn(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Check if game ended if not do aiTurn
        if(checkIfGameEnded().Equals("") && turn < 10)aiTurn();
        else if(checkIfGameEnded().Equals("Ai")) victor = "Player";
        else victor = "Ai";
        if(roundFinished) roundOverTimer += Time.deltaTime;
        //Win Stuff
        //First game Win Stuff (Part where you're supposed to win)
        if(GameManager.progress == 2){
            if(victor == "Player"){
                retry.gameObject.SetActive(true);
                rageText.text = "Lil Bro: Haha! You suck let's play again!";
            }
            else if(victor.Equals("Ai")){
                roundFinished = true;
                if(roundOverTimer >= 0.4f){
                    GameManager.progress++;
                    TextScript.dialogIndex++;
                    SceneManager.LoadScene("Cutscene");
                }
            }
        }
        //Best of 5 Stuff (Part where you're supposed to lose)
        else if(GameManager.progress == 4){
            if(victor == "Player"){
                if(aiWins < 3 && !roundFinished){
                    aiWins++; 
                    broText.text = "Lil Bro (O): " + aiWins;
                    roundFinished = true;
                }
                //Ai win round
                if(roundOverTimer >= 0.4f){
                    if(aiWins == 3){
                        if(PlayerPrefs.GetInt("beaten", 0) == 0) PlayerPrefs.SetInt("beaten", 1);
                        GameManager.progress++;
                        TextScript.dialogIndex++;
                        SceneManager.LoadScene("Cutscene");
                    }
                    else{
                        SceneManager.LoadScene("Game Scene");
                    }
                }   
            }
            else if(victor.Equals("Ai")){
                if(playerWins < 3 && !roundFinished){
                    playerWins++;
                    youText.text = "Big Bro (X): " + playerWins;
                    roundFinished = true;
                }
                if(playerWins == 3){
                    youText.text = "Big Bro (X): " + playerWins;
                    retry.gameObject.SetActive(true);
                    rageText.text = "Lil Bro: DAD HE'S CHEATING AGAIN!";
                }
                if(roundOverTimer >= 1){
                    if(playerWins < 3){
                        SceneManager.LoadScene("Game Scene");
                    }
                }
                
            }
        }
        //Endless
        else{
            if(victor.Equals("Player")){
                //Ai win round
                if(!roundFinished){
                    aiWins++; 
                    broText.text = "Lil Bro (O): " + aiWins;
                    roundFinished = true;
                }
                if(roundOverTimer >= 1)SceneManager.LoadScene("Game Scene");
            }
            else if(victor.Equals("Ai")){
            //Player win round
                if(!roundFinished){
                    playerWins++;
                    youText.text = "Big Bro (X): " + playerWins;
                    roundFinished = true;
                }
                if(roundOverTimer >= 1) SceneManager.LoadScene("Game Scene");
            }
        }
    }

    public void aiTurn(){  
        aiGrid = grid;
        //Turn 1
        if(turn == 1){
            int startingRNG = Random.Range(1,100);
            //Cardinal Start
            if(startingRNG <= 50){
                int cardinalChoice = Random.Range(0,4);
                switch(cardinalChoice){
                    case 0:
                        selectionX = 1;
                        selectionY = 0;
                        break;
                    case 1:
                        selectionX = 0;
                        selectionY = 1;
                        break;
                    case 2:
                        selectionX = 2;
                        selectionY = 1;
                        break;
                    case 3:
                        selectionX = 1;
                        selectionY = 2;
                        break;
                }
            }else{ //Corner Start
                int cornerChoice = Random.Range(0,4);
                switch(cornerChoice){
                    case 0:
                        selectionX = 0;
                        selectionY = 0;
                        break;
                    case 1:
                        selectionX = 2;
                        selectionY = 0;
                        break;
                    case 2:
                        selectionX = 0;
                        selectionY = 2;
                        break;
                    case 3:
                        selectionX = 2;
                        selectionY = 2;
                        break;
                }
            }

            

            /**
            selectionX = Random.Range(0,3);
            if(selectionX != 1) selectionY = Random.Range(0,3);
            else{  //Avoid Choosing middle spot
                if(Random.Range(0,2) == 0) selectionY = 0;
                else selectionY = 2;
            } 
            **/

            turn++;
            grid[selectionY,selectionX] = 'O';
        }
        //Turn 3+
        else if (turn > 2 && turn % 2 == 1){
            turn++;
            aiChecks();
        }
        
    }

    public static string checkIfGameEnded(){
        //Check for wins
        for(int i = 0; i < 3; i++){
            //Row
            if(grid[i,0] != '_' && grid[i,0] == grid[i,1] && grid[i,1] == grid[i,2]){
                winningWay = "row" + i;
                if(grid[i,0] == 'X') return "Player";
                else if(grid[i,0] == 'O') return "Ai";
            }
            //Columns
            else if(grid[0,i] != '_' && grid[0,i] == grid[1,i] && grid[1,i] == grid[2,i]){
                winningWay = "col" + i;
                if(grid[0,i] == 'X') return "Player";
                else if(grid[0,i] == 'O') return "Ai";
            }
        }
        //Diagonals
        if(grid[0,0] != '_' && grid[0,0] == grid[1,1] && grid[1,1] == grid[2,2]){
            winningWay = "diagDR";
            if(grid[0,0] == 'X') return "Player";
            else if(grid[0,0] == 'O') return "Ai";
        }
        else if(grid[0,2] != '_' && grid[0,2] == grid[1,1] && grid[1,1] == grid[2,0]){
            winningWay = "diagUR";
            if(grid[0,2] == 'X') return "Player";
            else if(grid[0,2] == 'O') return "Ai";
        }
        return "";
    }

    public void aiChecks(){
        float[,] aiScores = new float[3,3];
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                //Check if slot is empty and not the middle unless its final turn
                if(grid[i,j] != '_') aiScores[i,j] += -9000;
                //Dislike Middle and prefer cardinals
                aiScores[1,1] += -0.1f;
                aiScores[0,1] += 0.1f;
                aiScores[1,0] += 0.1f;
                aiScores[1,2] += 0.1f;
                aiScores[2,1] += 0.1f;
                // && ((i != 1 && j != 1))
                if(grid[i,j] == '_' ){
                    char[,] aiGrid = (char[,])grid.Clone();
                    aiGrid[i,j] = 'O';
                    //Check if it makes ai win
                    if(aiGrid[i,0] == 'O' && aiGrid[i,1] == 'O' && aiGrid[i,2] == 'O') aiScores[i,j] += -100;
                    if(aiGrid[0,j] == 'O' && aiGrid[1,j] == 'O' && aiGrid[2,j] == 'O') aiScores[i,j] += -100;
                    if(aiGrid[0,0] == 'O' && aiGrid[1,1] == 'O' && aiGrid[2,2] == 'O') aiScores[i,j] += -100;
                    if(aiGrid[0,2] == 'O' && aiGrid[1,1] == 'O' && aiGrid[2,0] == 'O') aiScores[i,j] += -100;
                    //Check other stuff

                    int xNum = 0;
                    int oNum = 0;
                    //Check for Number of X's and O's in spots row
                    for(int k = 0; k < 3; k++){
                        if(grid[i,k] == 'O') oNum++;
                        else if(grid[i,k] == 'X') xNum++;
                    }
                    //If would be blocking a Player Win
                    if(xNum == 2 && oNum == 0) aiScores[i,j] += -2;
                    //If would be in a now blocked row
                    if(xNum == 1 && oNum == 1) aiScores[i,j] += 1.5f;
                    //If would be causing a 2 in a row
                    if(xNum == 0 && oNum == 1) aiScores[i,j] += -1.5f;

                    //Check for Number of X's and O's in spots column
                    xNum = 0;
                    oNum = 0;
                    //Check for Number of X's and O's in spots row
                    for(int k = 0; k < 3; k++){
                        if(grid[k,j] == 'O') oNum++;
                        else if(grid[k,j] == 'X') xNum++;
                    }
                    //If would be blocking a Player Win
                    if(xNum == 2 && oNum == 0) aiScores[i,j] += -2;
                    //If would be in a now blocked column
                    if(xNum == 1 && oNum == 1) aiScores[i,j] += 1.5f;
                    //If would be causing a 2 in a column
                    if(xNum == 0 && oNum == 1) aiScores[i,j] += -1.5f;

                    //Check Diaganols
                    if( (i == 0 && (j == 0 || j == 2) ) || (i == 2 && (j == 0 || j == 2) ) ){
                        
                        if( (i == 0 && j == 0) || (i == 2 && j == 2)){
                            xNum = 0;
                            oNum = 0;
                            if(grid[0,0] == 'X') xNum++;
                            else if(grid[0,0] == 'O') oNum++;
                            if(grid[1,1] == 'X') xNum++;
                            else if(grid[1,1] == 'O') oNum++;
                            if(grid[2,2] == 'X') xNum++;
                            else if(grid[2,2] == 'O') oNum++;

                            //If would be blocking a Player Win
                            if(xNum == 2 && oNum == 0) aiScores[i,j] += -2;
                            //If would be in a now blocked column
                            if(xNum == 1 && oNum == 1) aiScores[i,j] += 1.5f;
                            //If would be causing a 2 in a column
                            if(xNum == 0 && oNum == 1) aiScores[i,j] += -1.5f;
                        }
                        else if((i == 2 && j == 0) || (i == 0 && j == 2)){
                            xNum = 0;
                            oNum = 0;
                            if(grid[2,0] == 'X') xNum++;
                            else if(grid[2,0] == 'O') oNum++;
                            if(grid[1,1] == 'X') xNum++;
                            else if(grid[1,1] == 'O') oNum++;
                            if(grid[0,2] == 'X') xNum++;
                            else if(grid[0,2] == 'O') oNum++;

                            //If would be blocking a Player Win
                            if(xNum == 2 && oNum == 0) aiScores[i,j] += -2;
                            //If would be in a now blocked column
                            if(xNum == 1 && oNum == 1) aiScores[i,j] += 1.5f;
                            //If would be causing a 2 in a column
                            if(xNum == 0 && oNum == 1) aiScores[i,j] += -1.5f;
                        }
                    }    
                }
            }
        }
        //Search through aiScores to find highest value then save indices to choose were ai goes
            int MaxI = 0;
            int MaxJ = 0;
            float currentMax = float.MinValue;
            for(int i = 0; i < 3; i++){
                for(int j = 0; j < 3; j++){
                    if(aiScores[i,j] > currentMax){
                        currentMax = aiScores[i,j];
                        MaxI = i;
                        MaxJ = j;
                    }
                    else if(aiScores[i,j] == currentMax){
                        if(Random.Range(0,2) == 1){
                            currentMax = aiScores[i,j];
                            MaxI = i;
                            MaxJ = j;
                        }
                    }
                }
            }
            Debug.Log(MaxI + "," + MaxJ);
            grid[MaxI,MaxJ] = 'O';
            theAiGrid = aiScores;
    }
}
