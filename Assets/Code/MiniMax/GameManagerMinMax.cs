using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMinMax : GameManager
{
    [SerializeField] private MiniMaxTicTacToe miniMax;

    public override void SwitchTurn()
    {
        base.SwitchTurn();

        if (turnOf == TileState.O)
        {
            OnTilePressed(miniMax.BestMove(gameBoard));
        }
    }

    /// <summary>
    /// Calculates the game score of the given game board state.
    /// </summary>
    public int GetGameScore(TileState[,] gameBoard)
    {
        foreach (Vector2Int[] item in winningLines)
        {
            TileState _startState = gameBoard[item[0].x, item[0].y];
            if (_startState != TileState.none)
            {
                if (gameBoard[item[1].x, item[1].y] == _startState && gameBoard[item[2].x, item[2].y] == _startState)
                {
                    switch (_startState)
                    {
                        case TileState.X:
                            return -10;
                        case TileState.O:
                            return +10;
                    }
                }
            }
        }
        return 0;
    }
}
