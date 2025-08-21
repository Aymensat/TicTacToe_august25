using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class AI_playing : MonoBehaviour
{


    public GameLogic gameLogic;


    public int[] besMove = new int[2]; 

    public int MyMinMax(List<List<int>> G , int minmax)
    {
        // minmax =1  , indicating max playing 

        string gameState = CheckGameState( G );

        if (gameState == "X") return 1;
        else if (gameState == "O") return -1; 
        else if(gameState == "Draw") return 0;
        
        else if(gameState =="Pending")
        {

            List<List<List<int>>> gameBranches = new List<List<List<int>>>();

            List<int> gameBranchesResults = new List<int>();   

            List<List<int>> temp_G;


            for (int i  = 0; i <  3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(G[i][j] == 0 )
                    {

                        temp_G = G_DeepCopy(G);
                        temp_G[i][j] = minmax;
                        gameBranches.Add(temp_G);


                        

                    }
                }
            }


            foreach (var g in gameBranches)
            {
                gameBranchesResults.Add(MyMinMax(g, -minmax));
            }

            int bestIndex;

            bestIndex = (minmax == 1)  ? gameBranchesResults.IndexOf(gameBranchesResults.Max()) : gameBranchesResults.IndexOf(gameBranchesResults.Min()); 

            temp_G = G_DeepCopy(gameBranches[bestIndex] );

            for(int i = 0;i<3; i++)
            {
                for (int j = 0;j < 3; j++)
                {
                    if( G[i][j] != temp_G[i][j] && temp_G[i][j] == minmax)
                    {
                        besMove[0] = i; besMove[1]= j;
                        break; 
                    }
                }
            }

            return (minmax == 1) ? gameBranchesResults.Max() : gameBranchesResults.Min();


        }
        
        else return -100; 
    }

    /// tw juuuust extracti kifeh tarba7...ez


    private string CheckGameState(List<List<int>> G)
    {


        // Row  winning 
        for (int i = 0; i < 3; i++)
        {
            if (G[i][0] != 0 && G[i][0] == G[i][1] && G[i][0] == G[i][2])
            {

                return (G[i][0] == 1) ? "X" : "O";
            }
        }


        // Column winning
        for (int i = 0; i < 3; i++)
        {
            if (G[0][i] != 0 && G[0][i] == G[1][i] && G[0][i] == G[2][i])
            {

                return (G[0][i] == 1) ? "X" : "O";
            }
        }



        // Diagonal checks
        if (G[0][0] != 0 && G[0][0] == G[1][1] && G[0][0] == G[2][2])
        {
 
            return (G[0][0] == 1) ? "X" : "O";
        }

        if (G[0][2] != 0 && G[0][2] == G[1][1] && G[0][2] == G[2][0])
        {

            return (G[0][2] == 1) ? "X" : "O";
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


    private List<List<int>> G_DeepCopy(List<List<int>> list) 
    
    {
        List<List<int>> L = new List<List<int>>();
        foreach (List<int> row in list)
        {
            var newRow = new List<int>(row);
            L.Add(newRow);
        }
        return L;
    }

}
