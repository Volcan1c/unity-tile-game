using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTilemapController : TilemapController
{
    [SerializeField] TileBase moveHighlight;

    public void DrawMoveHighlights(List<Vector3Int> positions)
    {
        foreach (Vector3Int position in positions)
        {
            tilemap.SetTile(position, moveHighlight);
        }
    }

    public void ClearHighlights()
    {
        tilemap.ClearAllTiles();
    }
}
