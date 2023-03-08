using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxTicTacToe : MonoBehaviour
{
    [SerializeField] private GameManagerMinMax gameManager;

    /// <summary>
    /// Gets a list of tiles and returns a list of states.
    /// </summary>
    private TileState[,] GetBoardState(Tile[,] gameBoard)
    {
        TileState[,] _boardState = new TileState[3, 3];

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _boardState[x, y] = gameBoard[x, y].GetTileState();
            }
        }
        return _boardState;
    }

    /// <summary>
    /// Returns true if there is a move left.
    /// </summary>
    private bool MovesLeft(TileState[,] boardState)
    {
        foreach (TileState item in boardState)
        {
            if (item == TileState.none)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns the best move. 
    /// The move is calculated with the minimax algorithm.
    /// </summary>
    public Vector2Int BestMove(Tile[,] gameBoard)
    {
        //Get the state off the tiles
        TileState[,] _boardState = GetBoardState(gameBoard);

        int _bestScore = -1000;
        Vector2Int _bestMove = new Vector2Int(-1, -1);

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                //Check if move is valid
                if (_boardState[x, y] == TileState.none)
                {
                    //Set move
                    _boardState[x, y] = TileState.O;

                    //Start min max calculation
                    int _moveScore = MiniMax(_boardState, 0, false);

                    //Remove move for next calculation.
                    _boardState[x, y] = TileState.none;

                    //Check if score is better than other scores.
                    if (_moveScore > _bestScore)
                    {
                        _bestScore = _moveScore;
                        _bestMove = new Vector2Int(x, y);
                    }
                }
            }
        }

        //Return best move
        return _bestMove;
    }

    private int MiniMax(TileState[,] boardState, int depth, bool isMax)
    {
        //Check if the game is over.
        int _gameScore = gameManager.GetGameScore(boardState);

        //if maximum won the game
        if (_gameScore == +10 || _gameScore == -10)
        {
            return _gameScore;
        }
        
        //if it is a tie
        if (MovesLeft(boardState) == false)
        {
            return 0;
        }

        //Max calculation.
        if (isMax)
        {
            int _bestScore = -1000;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    //Check if move is valid
                    if (boardState[x,y] == TileState.none)
                    {
                        //Set move
                        boardState[x, y] = TileState.O;
                       
                        //Calculate move score
                        _bestScore = Mathf.Max(_bestScore, MiniMax(boardState, depth + 1, !isMax));

                        //Remove move for next calculation.
                        boardState[x, y] = TileState.none;
                    }
                }
            }
            return _bestScore;
        }
        //Min calculation.
        else
        {
            int _bestScore = 1000;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (boardState[x, y] == TileState.none)
                    {
                        //Set move
                        boardState[x, y] = TileState.X;

                        //Calculate move score
                        _bestScore = Mathf.Min(_bestScore, MiniMax(boardState, depth + 1, !isMax));

                        //Remove move for next calculation.
                        boardState[x, y] = TileState.none;
                    }
                }
            }
            return _bestScore;
        }
    }
}
