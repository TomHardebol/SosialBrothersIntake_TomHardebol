using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxTile : Tile
{
    public override void OnMouseDown()
    {
        GameManager.Instance.OnTilePressed(id);
    }
}
