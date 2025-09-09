using System.Collections.Generic;
using UnityEngine;

public class AIStep : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] int[] squareValues;
    TicTacToe gameScript;


    void Start()
    {
        gameScript = gameManager.GetComponent<TicTacToe>();
        squareValues = new int[9];
    }
    public void DoAIStep()
    {
        // Read the board
        gameScript.ReadBoard(ref squareValues);

        // Evaluate the info

        // Choose an action
        bool inloop = true;
        while (inloop)
        {
            int rand = Random.Range(0, 9);
            if (squareValues[rand] == 0)
            {
                gameScript.SelectSquare(rand);
                inloop = false;
            }
        }
        
        
        
    }
}
