using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used on the tile prefabs. 
/// Its responsible for the visual presentation of the game and it saves its state.
/// </summary>
public class Tile : MonoBehaviour
{
    // Values for the tile that need to be set via unity.
    // I used this instead of getting the child's end component because its more efficient.
    [SerializeField] private GameObject visualO, visualX;
    [SerializeField] private Collider2D collider2D;

    //Values that need to be saved for the game manager.
    private TileState myState = TileState.none;
    private protected Vector2Int id;
    
    public virtual void OnMouseDown()
    {
        GameManager.Instance.OnTilePressed(id);
    }

    public TileState GetTileState()
    {
        return myState;
    }

    public void SetID(Vector2Int newID)
    {
        id = newID;
    }

    public void SetCollider(bool targetState)
    {
        collider2D.enabled = targetState;
    }

    /// <summary>
    /// This function changes the visual, state and collider based on the given tile type value.
    /// </summary>
    public void SetVisual(TileState targetState)
    {
        switch (targetState)
        {
            case TileState.none:
                visualO.SetActive(false);
                visualX.SetActive(false);
                myState = TileState.none;
                break;
            case TileState.X:
                visualX.SetActive(true);
                SetCollider(false);
                myState = TileState.X;

                break;
            case TileState.O:
                visualO.SetActive(true);
                SetCollider(false);
                myState = TileState.O;
                break;
        }
    }
}
