﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTilemap : TilemapController
{
    [SerializeField] TileBase moveHighlight;
    [SerializeField] TileBase attackHighlight;

    public void DrawMoveHighlight(List<Vector3Int> positions)
    {
        foreach (Vector3Int position in positions)
        {
            tilemap.SetTile(position, moveHighlight);
        }
    }

    public void DrawAttackHighlight(List<Vector3Int> positions)
    {
        foreach (Vector3Int position in positions)
        {
            tilemap.SetTile(position, attackHighlight);
        }
    }

    public void ClearHighlights()
    {
        tilemap.ClearAllTiles();
    }
}
