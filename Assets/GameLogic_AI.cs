using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic_AI : MonoBehaviour

{
    public LineRenderer line;
    public AI_playing myAI;

    public Button[] cellButtons;
    public Sprite xSprite;
    public Sprite oSprite;

    public int X_Score , O_Score;

    public Score ScoreDisplay; 

    private string winning_direction;

    private int winning_coord;

    List<List<int>> G = new List<List<int>>()
    {
        new List<int> { 0 ,0 ,0 },
        new List<int> { 0 ,0 ,0 },
        new List<int> { 0 ,0 ,0 },
    };


    char currentPlayer = 'X';
    string currentGameState;

    int last_clickled_row = -1;
    int last_clickled_col = -1;

    bool preferStarting; 
    void Start()
    {
        preferStarting = true;
        GameReset();
        ScoreReset();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnCellClicked(int grid)
    {
        if (preferStarting && currentPlayer == 'X' || !preferStarting && currentPlayer == 'O')

        {

            Debug.Log("we are playing human even tho  preferStarting = " + preferStarting + "currentPlayer = " + currentPlayer );

            last_clickled_row = grid / 3;
            last_clickled_col = grid % 3;

            //logic here 

            if (ValidMove(last_clickled_row, last_clickled_col))
            {

                Button clickedButton = cellButtons[grid];
                Image buttonImage = clickedButton.GetComponent<Image>();

                buttonImage.sprite = (currentPlayer == 'X') ? xSprite : oSprite;


                clickedButton.interactable = false;

                G[last_clickled_row][last_clickled_col] = (currentPlayer == 'X') ? 1 : -1;


                CheckDrawingLine();

                if(currentGameState=="Pending")
                {currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';

                    play(myAI);
                }

            }



            //else meaning : move not valid ( exsiting square ) 
            else { } // i dont i will add this later as the button is already not interactable 
        }
       
            
    }



    public void GameReset()
    {

        line.enabled = false;
        Image buttonImage;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++) { G[i][j] = 0;  }

        }

        foreach(Button button in cellButtons)
        {
            buttonImage = button.GetComponent<Image>();
            buttonImage.sprite = null;
            button.interactable = true ;
        }
        currentPlayer = 'X';
        winning_direction = null;
        winning_coord = -1;
        currentGameState = "Pending";
        // intended no score reset 

        if (!preferStarting)
        {
            Debug.Log("MY BOY WANTS TO PLAY"); 

            play(myAI); 
        }

    }

    public void ScoreReset()
    {
        O_Score = 0;
        X_Score = 0;
        ScoreDisplay.UpdateScoreDisplay(0, 0); 

    }

            
    public void GoToMainMenu()
    {

        SceneManager.LoadScene("MainMenu"); 

    }

    public void WhoStart(int k)
    {
        preferStarting = (k ==0 ) ? true : false;
    }

    
    public void SwitchWhoStart(bool p)
    {
        preferStarting = p;
        Debug.Log(preferStarting);
        GameReset();
        ScoreReset();
    }


    private string CheckGameState()
    {


        // Row  winning 
        for (int i = 0; i < 3; i++)
        {
            if (G[i][0] != 0 && G[i][0] == G[i][1] && G[i][0] == G[i][2])
            {
                winning_direction = "row";
                winning_coord = i;
                return (currentPlayer == 'X') ? "X" : "O";
            }
        }


        // Column winning
        for (int i = 0; i < 3; i++)
        {
            if (G[0][i] != 0 && G[0][i] == G[1][i] && G[0][i] == G[2][i])
            {
                winning_direction = "col";
                winning_coord = i;
                return (currentPlayer == 'X') ? "X" : "O"; 
            }
        }



        // Diagonal checks
        if (G[0][0] != 0 && G[0][0] == G[1][1] && G[0][0] == G[2][2])
        {
            winning_direction = "diag";
            return (currentPlayer == 'X') ? "X" : "O";
        }

        if (G[0][2] != 0 && G[0][2] == G[1][1] && G[0][2] == G[2][0])
        {
            winning_direction = "reverse_diag";
            return (currentPlayer == 'X') ? "X" : "O";
        }

        // Check for pending moves
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (G[i][j] == 0)
                    return "Pending";
            }
        }

        return "Draw";
    }

    private void DrawLine(Vector3 start, Vector3 end)

    {
        Debug.Log("normalement fema line ! at " + start + end); 
        line.enabled = true;

        line.positionCount = 2;

        line.SetPosition(0, start); 
        line.SetPosition(1, end);


    }

    private bool ValidMove(int x, int y)
    {

        return (G[x][y] == 0) ? true : false;
    }

    private Vector3[] GetWinningPoints()
    {
        Vector3[] s = new Vector3[2];

        if( winning_direction == "diag")
        {
            s[0] = cellButtons[0].transform.position;
            s[1] = cellButtons[8].transform.position;
        }

        if(  winning_direction == "reverse_diag")
        {
            s[0] = cellButtons[6].transform.position;
            s[1] = cellButtons[2].transform.position;

        }

        if(winning_direction =="row")
        {
            s[0] = cellButtons[winning_coord*3].transform.position;
            s[1] = cellButtons[winning_coord * 3  +2 ].transform.position;
        }

        if (winning_direction == "col")
        {
            s[0] = cellButtons[winning_coord].transform.position;
            s[1] = cellButtons[winning_coord +6 ].transform.position;

        }


        return s;
    }



    private void play(AI_playing myAI)
    {
        int tmp;
        tmp = (preferStarting == true) ? -1 : 1;
        myAI.MyMinMax(G , tmp);

        Debug.Log("Ai best move is " + myAI.besMove);

        int grid = myAI.besMove[0]*3 + myAI.besMove[1];

        Button clickedButton = cellButtons[grid];
        Image buttonImage = clickedButton.GetComponent<Image>();

        buttonImage.sprite = (currentPlayer == 'X') ? xSprite : oSprite;


        clickedButton.interactable = false;

        G[myAI.besMove[0]][myAI.besMove[1]] = (currentPlayer == 'X') ? 1 : -1;

        CheckDrawingLine();

        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';

    }





    private void CheckDrawingLine()
    {
        currentGameState = CheckGameState();
        Debug.Log(currentGameState);




        if (currentGameState == "X" || currentGameState == "O")
        {

            Vector3[] v = GetWinningPoints();
            DrawLine(v[0], v[1]);

            if (currentGameState == "X") X_Score++;
            else O_Score++;

            ScoreDisplay.UpdateScoreDisplay(X_Score, O_Score);
        }
    }

}
