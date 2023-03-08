using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    //The sort of tile that is used for the board.
    [SerializeField] private GameObject tile;

    protected Tile[,] gameBoard = new Tile[3, 3];

    protected TileState turnOf = TileState.X;

    //This is a list of all the winning lines. This is used to check if a game is won.
    protected List<Vector2Int[]> winningLines = new List<Vector2Int[]>() { 
            //Row
            new Vector2Int[3]{ new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0)},
            new Vector2Int[3]{ new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1)},
            new Vector2Int[3]{ new Vector2Int(0,2), new Vector2Int(1,2), new Vector2Int(2,2)},
            //Column
            new Vector2Int[3]{ new Vector2Int(0,0), new Vector2Int(0,1), new Vector2Int(0,2)},
            new Vector2Int[3]{ new Vector2Int(1,0), new Vector2Int(1,1), new Vector2Int(1,2)},
            new Vector2Int[3]{ new Vector2Int(2,0), new Vector2Int(2,1), new Vector2Int(2,2)},
            //Diagonal
            new Vector2Int[3]{ new Vector2Int(0,0), new Vector2Int(1,1), new Vector2Int(2,2)},
            new Vector2Int[3]{ new Vector2Int(0,2), new Vector2Int(1,1), new Vector2Int(2,0)},
    };

    private void Start()
    {
        CreateBoard();
    }

    /// <summary>
    /// Creates the board and saves the board tiles in a list.
    /// </summary>
    private void CreateBoard()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                GameObject _newTile = GameObject.Instantiate(tile, new Vector3(0 + (2.2f * x), 0 - (2.2f * y)), Quaternion.identity);
                _newTile.name = $"tile: x:{x}, y:{y}";
                gameBoard[x, y] = _newTile.GetComponent<Tile>();
                gameBoard[x, y].SetID(new Vector2Int(x, y));
            }
        }
    }

    /// <summary>
    /// Resets the tiles and makes them intractable.
    /// </summary>
    public void ResetBoard()
    {
        foreach (Tile item in gameBoard)
        {
            item.SetVisual(TileState.none);
            item.SetCollider(true);
        }
    }

    /// <summary>
    /// Function gets called when a tile is pressed.
    /// </summary>
    public void OnTilePressed(Vector2Int tileID)
    {
        TurnTile(tileID);

        CheckForWin();

        if (MovesLeft())
        {
            SwitchTurn();
        }
        else
        {
            UIManager.Instance.SetWhoWonText($"Game is tied");
            UIManager.Instance.SetRestartButton(true);
        }
    }

    /// <summary>
    /// Calculates if there are moves left on the board.
    /// </summary>
    private bool MovesLeft()
    {
        foreach (Tile item in gameBoard)
        {
            if (item.GetTileState() == TileState.none)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Calls the tile change function based on whose turn it is.
    /// </summary>
    private void TurnTile(Vector2Int tileID)
    {
        switch (turnOf)
        {
            case TileState.X:
                gameBoard[tileID.x, tileID.y].SetVisual(TileState.X);
                break;
            case TileState.O:
                gameBoard[tileID.x, tileID.y].SetVisual(TileState.O);
                break;
        }
    }

    /// <summary>
    /// Switch the turn and updates the turn of text.
    /// </summary>
    public virtual void SwitchTurn()
    {
        switch (turnOf)
        {
            case TileState.X:
                turnOf = TileState.O;
                break;
            case TileState.O:
                turnOf = TileState.X;
                break;
        }
        UIManager.Instance.SetWhosTurnText($"Turn of {turnOf}");
    }

    /// <summary>
    /// Checks for a win with the winning lines list.
    /// </summary>
    private void CheckForWin()
    {
        foreach (Vector2Int[] item in winningLines)
        {
            TileState _startState = gameBoard[item[0].x, item[0].y].GetTileState();
            if (_startState != TileState.none)
            {
                if (gameBoard[item[1].x, item[1].y].GetTileState() == _startState && gameBoard[item[2].x, item[2].y].GetTileState() == _startState)
                {
                    UIManager.Instance.SetWhoWonText($"Player {_startState} won");
                    UIManager.Instance.SetRestartButton(true);
                }
            }
        }
    }
}